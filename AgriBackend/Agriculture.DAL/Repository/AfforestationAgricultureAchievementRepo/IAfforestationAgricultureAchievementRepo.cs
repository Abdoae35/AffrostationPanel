namespace Agriculture.DAL.Repository.AfforestationAgricultureAchievementRepo;

public interface IAfforestationAgricultureAchievementRepo
{
    IQueryable<AfforestationAgricultureAchievement> GetFiltered(
        DateOnly? fromDate,
        DateOnly? toDate,
        int? userId,
        int? locationId,
        int? treeId);
    public IQueryable<AfforestationAgricultureAchievement?> GetAll();
    public Task<AfforestationAgricultureAchievement?> GetByIdAsync(int id);
    public Task Insert(AfforestationAgricultureAchievement? entity);
    public Task Update(AfforestationAgricultureAchievement? entity);
    public Task DeleteAsync(AfforestationAgricultureAchievement? entity);
}