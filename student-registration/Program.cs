using student_registration.BLL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStudent, StudentBLL>();
builder.Services.AddScoped<IDepartment, DepartmentBLL>();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");

app.Run();



/*  app.Use(async (context, next) =>
  {

     await context.Response.WriteAsync("Hello, \n \t ");
     await next.Invoke();
  });

  app.Run(async context =>
  {
      await context.Response.WriteAsync("world!");

  }); 
*/
