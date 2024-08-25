using Project.WebAPI.Models.DapperContext;
using Project.WebAPI.Repositories.AboutUsRepository;
using Project.WebAPI.Repositories.BrandRepository;
using Project.WebAPI.Repositories.CategoryRepository;
using Project.WebAPI.Repositories.HomeContentRepository;
using Project.WebAPI.Repositories.ModelRepository;
using Project.WebAPI.Repositories.OurServiceRepository;
using Project.WebAPI.Repositories.TestimonialRepository;
using Project.WebAPI.Repositories.VehicleRepository;

namespace Project.WebAPI.Containers
{
	public static class Extensions
	{
		public static void ContainerDependencies(this IServiceCollection services)
		{
			services.AddTransient<DapperContext>(); //Connection

			services.AddTransient<ICategoryRepository, CategoryRepository>();
			services.AddTransient<IBrandRepository, BrandRepository>();
			services.AddTransient<IModelRepository, ModelRepository>();
			services.AddTransient<IVehicleRepository, VehicleRepository>();
			services.AddTransient<IHomeContentRepository, HomeContentRepository>();
			services.AddTransient<IAboutUsRepository, AboutUsRepository>();
			services.AddTransient<IOurServiceRepository, OurServiceRepository>();
			services.AddTransient<ITestimonialRepository, TestimonialRepository>();
		}
	}
}
