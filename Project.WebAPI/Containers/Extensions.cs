using Project.WebAPI.Models.DapperContext;
using Project.WebAPI.Repositories.CategoryRepository;

namespace Project.WebAPI.Containers
{
	public static class Extensions
	{
		public static void ContainerDependencies(this IServiceCollection services)
		{
			services.AddTransient<DapperContext>(); //Connection


			services.AddTransient<ICategoryRepository, CategoryRepository>();
		}
	}
}
