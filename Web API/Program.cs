    using Data_Access_Layer;
    using Data_Access_Layer.Repositories;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.Filters;
    using System.Text;
    using Web_API;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    //Swagger authorization configuration. Çok anlamadým.
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oatuh2", new OpenApiSecurityScheme
        {
            Description = "Authorization header with bearer -> (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
        });


    builder.Services.AddDbContext<MusicShelfContext>(options =>
    options.UseSqlServer("Data Source=DESKTOP-NM069V3\\SQLEXPRESS;Initial Catalog=music_shelf_db;Integrated Security=True;TrustServerCertificate=True"));


    builder.Services.AddScoped<UserRepository>();
    builder.Services.AddScoped<SongRepository>();
    builder.Services.AddScoped<TokenService>();

    // token key'i almak için
    builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

    //auto mapper eklendi
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    

    //cors
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
                            {
                                builder.WithOrigins("http://localhost:3000")
                                .AllowCredentials()
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .WithExposedHeaders("Authorization"); 
                            });
    });
    
    //authentication, authorization configuration
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options=>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                     ValidateIssuer = false,
                     ValidateAudience = false,
            };
        });
    var app = builder.Build();
   

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
