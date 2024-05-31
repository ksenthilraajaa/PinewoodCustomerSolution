using PwCustomer.Client.ApiServices;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();


        builder.Services.AddHttpClient<CustomerApi>("LocalApi", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5166/");
        });

        // builder.Services.AddHttpClient<CustomerApi>("SecureApi", client =>
        // {
        //     client.BaseAddress = new Uri("https://localhost:7282/");
        // });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/")
            {
                context.Response.Redirect("/Customer/List");
                return;
            }
            await next();
        });

        app.MapControllerRoute(
            name: "default",
         pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}