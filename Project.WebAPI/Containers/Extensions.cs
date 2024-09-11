using Project.Shared.DTOs.Reviews;
using Project.WebAPI.Models.DapperContext;
using Project.WebAPI.Repositories.AboutUsRepository;
using Project.WebAPI.Repositories.AddressRepository;
using Project.WebAPI.Repositories.AwardRepository;
using Project.WebAPI.Repositories.BlogRepository;
using Project.WebAPI.Repositories.BrandRepository;
using Project.WebAPI.Repositories.CategoryRepository;
using Project.WebAPI.Repositories.CommentRepository;
using Project.WebAPI.Repositories.HomeContentRepository;
using Project.WebAPI.Repositories.MessageRepository;
using Project.WebAPI.Repositories.MissionVisionRepository;
using Project.WebAPI.Repositories.ModelRepository;
using Project.WebAPI.Repositories.OurServiceRepository;
using Project.WebAPI.Repositories.ReviewRepository;
using Project.WebAPI.Repositories.StatisticsRepository;
using Project.WebAPI.Repositories.TestimonialRepository;
using Project.WebAPI.Repositories.UserRepository;
using Project.WebAPI.Repositories.VehicleAmenityRepository;
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
			services.AddTransient<IAwardRepository, AwardRepository>();
			services.AddTransient<IMissionVisionRepository, MissionVisionRepository>();
			services.AddTransient<IAddressRepository, AddressRepository>();
			services.AddTransient<IMessageRepository, MessageRepository>();
			services.AddTransient<IBlogRepository, BlogRepository>();
			services.AddTransient<IVehicleAmenityRepository, VehicleAmenityRepository>();
			services.AddTransient<IReviewRepository, ReviewRepository>();
			services.AddTransient<ICommentRepository, CommentRepository>();
			services.AddTransient<IStatisticsRepository, StatisticsRepository>();
			services.AddTransient<IUserRepository, UserRepository>();
		}
	}
}
