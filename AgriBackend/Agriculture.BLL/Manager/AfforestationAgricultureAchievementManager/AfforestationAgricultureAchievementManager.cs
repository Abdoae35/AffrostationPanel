namespace Agriculiture.BLL.Manager.AfforestationAgricultureAchievementManager;

public class AfforestationAgricultureAchievementManager: IAfforestationAgricultureAchievementManager
{
    private readonly IAfforestationAgricultureAchievementRepo _repo;

    public AfforestationAgricultureAchievementManager(IAfforestationAgricultureAchievementRepo repo)
    {
        _repo = repo;
    }

    public PaginatedResult<AfforestationAgricultureAchievementReadDto>PaginatedSearch(AfforestationSearchRequest request,int page =1,int pageSize=10)
    {
        var skip = (pageSize * page) - pageSize;
       
        
        IQueryable<AfforestationAgricultureAchievement> query = _repo.GetAll()
            
            .Include(x => x.TreeType)
            .Include(x => x.TreeName)
            .Include(x => x.LocationName)
            .Include(x => x.LocationType)
            .Include(x => x.User);

        if (request.FromDate.HasValue)
            query = query.Where(x => x.DateOfPlanted >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(x => x.DateOfPlanted <= request.ToDate.Value);

        if (request.SelectedUserId.HasValue)
            query = query.Where(x => x.UserId == request.SelectedUserId);

        if (request.SelectedLocationId.HasValue)
            query = query.Where(x => x.LocationNameId == request.SelectedLocationId);

        if (request.SelectedTreeId.HasValue)
            query = query.Where(x => x.TreeNameId == request.SelectedTreeId);

        var items= query
            .Skip(skip)
            .Take(pageSize)
            .Select(x => new AfforestationAgricultureAchievementReadDto()
        {
            Id = x.Id,
            DateOfPlanted = x.DateOfPlanted,
            TreeName = x.TreeName.Name,
            LocationName = x.LocationName.Name,
            Number = x.Number,
            TreeNameId = x.TreeNameId,
            UserName = x.User.Name,
            TreeTypeId = x.TreeTypeId,
            TreeTypeName = x.TreeType.Type,
            ScientificName = x.TreeName.ScientificName
            
        }).ToList();

        var totalCount = query.Count();
        return new PaginatedResult<AfforestationAgricultureAchievementReadDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public List<AfforestationAgricultureAchievementReadDto> BetterSearch(AfforestationSearchRequest request)
    {
        var query = _repo.GetFiltered(
            request.FromDate,
            request.ToDate,
            request.SelectedUserId,
            request.SelectedLocationId,
            request.SelectedTreeId);

        return query.Select(x => new AfforestationAgricultureAchievementReadDto
        {
            Id = x.Id,
            DateOfPlanted = x.DateOfPlanted,
            TreeName = x.TreeName.Name,
            LocationName = x.LocationName.Name,
            Number = x.Number,
            TreeNameId = x.TreeNameId,
            UserName = x.User.Name,
            TreeTypeId = x.TreeTypeId,
            TreeTypeName = x.TreeType.Type,
            ScientificName = x.TreeName.ScientificName
        }).ToList();
    }




    public IQueryable<AfforestationAgricultureAchievementReadDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<AfforestationAgricultureAchievementReadDto?> GetByIdAsynco(int id)
    {
        var model = await _repo.GetByIdAsync(id);
        if (model != null)
        {
            var dtoModel = new AfforestationAgricultureAchievementReadDto()
            {
                Id = model.Id,
                DateOfPlanted = model.DateOfPlanted,
                LocationName = model.LocationName.Name,
                TreeName = model.TreeName.Name,
                Number = model.Number,
                TreeNameId = model.TreeNameId,
                UserName = model.User.Name,
                TreeTypeName =model.TreeType.Type,
                TreeTypeId = model.TreeTypeId,
                LocationId = model.LocationNameId,
                LocationTypeId = model.LocationTypeId,
                LocationTypeName = model.LocationType.LocationType,
                ScientificName = model.TreeName.ScientificName
                
            };
            return  dtoModel;
        }
        
        return null;
        
    }


    public async Task Update(AfforestationUpdateDto dto)
    {
        var model = await _repo.GetByIdAsync(dto.Id);
        if (model == null) return;

        if (dto.DateOfPlanted != null) 
            model.DateOfPlanted = dto.DateOfPlanted.Value;

        if (dto.LocationTypeId.HasValue) 
            model.LocationTypeId = dto.LocationTypeId.Value;

        if (dto.TreeTypeId.HasValue) 
            model.TreeTypeId = dto.TreeTypeId.Value;

        if (dto.TreeNameId.HasValue) 
            model.TreeNameId = dto.TreeNameId.Value;

        if (dto.LocationId.HasValue) 
            model.LocationNameId = dto.LocationId.Value;

        if (dto.Number.HasValue) 
            model.Number = dto.Number.Value;

        await _repo.Update(model);

    }

    public async Task insert(AfforestationAddDto afforestationAddDto)
    {
        var newAgri = new AfforestationAgricultureAchievement()
        {
            DateOfPlanted = afforestationAddDto.DateOfPlanted,
            TreeTypeId = afforestationAddDto.TreeTypeId,
            LocationTypeId = afforestationAddDto.LocationTypeId,
            Number = afforestationAddDto.Number,
            TreeNameId = afforestationAddDto.TreeNameId,
            LocationNameId = afforestationAddDto.LocationNameId,
            UserId = afforestationAddDto.UserId
        };
        await _repo.Insert(newAgri);
    }

    public async Task Delete(AfforestationAgricultureAchievementReadDto deleteDto)
    {
       var model = await _repo.GetByIdAsync(deleteDto.Id);
       if (model != null) 
       {
            await _repo.DeleteAsync(model);
       }
       
       
    }
}