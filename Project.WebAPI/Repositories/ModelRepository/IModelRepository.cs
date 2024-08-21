using Project.Shared.DTOs.ModelDtos;

namespace Project.WebAPI.Repositories.ModelRepository
{
	public interface IModelRepository
	{
		Task InsertModel(InsertModelDto insertModelDto);
		Task<List<ResultModelDto>> ListAllModels();
		Task<List<ResultModelDto>> ListActiveModels();
		Task UpdateModel(UpdateModelDto updateModelDto);
		Task RemoveModel(int id);
		Task<ResultModelDto> GetModelById(int id);
	}
}
