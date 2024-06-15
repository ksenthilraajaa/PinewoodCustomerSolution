namespace PwCustomer.Api;

using Microsoft.OpenApi.Models;
using PwCustomer.Application;
using PwCustomer.Infrastructure;
using PwCustomer.Domain;


public class Startup
{

    private IConfiguration Configuration { get; }

    private IWebHostEnvironment Environment;

    private ILogger<Startup> _logger;

    public Startup(IWebHostEnvironment env, IConfiguration configuration, ILogger<Startup> logger)
    {
        Configuration = configuration;
        Environment = env;
        _logger = logger;

        _logger.LogInformation("Configuring app settings");

        var builder = new ConfigurationBuilder()
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();

        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        _logger.LogInformation("Adding Swagger configuration");
        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Pinewood Customer Api",
                    Version = "v1",
                    Description = "Pinewood Customer Api"
                });
            }
        );

        services.AddHealthChecks();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddInfraServices();

        _logger.LogInformation("Adding routing configurations..");
        services.AddRouting(options => options.LowercaseUrls = true);

        _logger.LogInformation("Check environment and Add Inmemory");

        if (Environment.IsDevelopment())
        {
            services.AddDistributedMemoryCache();
        }
    }

    public void Configure(IApplicationBuilder app)
    {
        _logger.LogInformation("Adding Inmemory database context");
        var customerdbContext = app.ApplicationServices.GetRequiredService<CustomerDbContext>();

        _logger.LogInformation("Adding Initial data for the inmemory database");
        AddInitialData(customerdbContext);

        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerApi v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthorization();

        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/")
            {
                context.Response.Redirect("/Swagger/Index.html");
                return;
            }
            await next();
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseHealthChecks("/health");
    }

    private static void AddInitialData(CustomerDbContext customerDbContext)
    {
        customerDbContext.Customers.Add(new Customer { Id = 1, FirstName = "John", LastName = "Walter", City = "London", Email = "John.Walter@hotmail.com", PhoneNumber = 07398762341 });
        customerDbContext.Customers.Add(new Customer { Id = 2, FirstName = "Andrew", LastName = "Barker", City = "Coventry", Email = "Andrew.Barker@gmail.com", PhoneNumber = 072428762341 });
        customerDbContext.Customers.Add(new Customer { Id = 3, FirstName = "James", LastName = "Cameron", City = "Birmingham", Email = "James.Cameron@yahoo.com", PhoneNumber = 07555762341 });
        customerDbContext.Customers.Add(new Customer { Id = 4, FirstName = "Dwayne", LastName = "Johnson", City = "Rugby", Email = "Dwayne.Johnson@inmail.com", PhoneNumber = 07396662341 });
        customerDbContext.Customers.Add(new Customer { Id = 5, FirstName = "Brad", LastName = "Pitt", City = "Manchester", Email = "Brad.Pitt@outlook.com", PhoneNumber = 07383462341 });
        customerDbContext.SaveChanges();
    }

}
