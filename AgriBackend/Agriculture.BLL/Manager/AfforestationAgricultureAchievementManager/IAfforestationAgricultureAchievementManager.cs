namespace Agriculiture.BLL.Manager.AfforestationAgricultureAchievementManager;

public interface IAfforestationAgricultureAchievementManager
{
    public PaginatedResult<AfforestationAgricultureAchievementReadDto> PaginatedSearch(AfforestationSearchRequest request, int page, int pageSize);
    
   
    public List<AfforestationAgricultureAchievementReadDto> BetterSearch(AfforestationSearchRequest request);
    
    public IQueryable<AfforestationAgricultureAchievementReadDto> GetAll();
    public Task<AfforestationAgricultureAchievementReadDto> GetByIdAsynco(int id);
    public Task Update(AfforestationUpdateDto afforestationUpdateDto);
    public Task insert(AfforestationAddDto afforestationAddDto);
    public Task Delete(AfforestationAgricultureAchievementReadDto deleteDto);
    
}