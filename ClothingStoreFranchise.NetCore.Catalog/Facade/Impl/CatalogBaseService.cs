using AutoMapper;
using ClothingStoreFranchise.NetCore.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Facade.Impl
{
    public abstract class CatalogBaseService<TEntity, TAppId, TEntityDto, TEntityDao>
        where TEntity : Entity<TAppId>
        where TEntityDto : IEntityDto<TAppId>
        where TEntityDao : IDao<TEntity, TAppId>
    {
        protected readonly TEntityDao _entityDao;
        protected readonly IMapper _mapper;

        protected CatalogBaseService(TEntityDao entityDao, IMapper mapper)
        {
            _entityDao = entityDao;
            _mapper = mapper;
        }

        #region "Create"

        public async virtual Task<TEntityDto> CreateAsync(TEntityDto dto)
        {
            //await CreateValidationActionsAsync(dto);
            TEntity entity = _mapper.Map<TEntity>(dto);
            return await CreateActionsAsync(entity);
        }

        public async virtual Task<ICollection<TEntityDto>> CreateAsync(ICollection<TEntityDto> dtos)
        {
            var entities = new List<TEntity>();
            foreach (TEntityDto dto in dtos)
            {
                //await CreateValidationActionsAsync(dto);
                TEntity entity = _mapper.Map<TEntity>(dto);
                entities.Add(entity);
            }
            return await CreateActionsAsync(entities);
        }

        protected async virtual Task CreateValidationActionsAsync(TEntityDto dto)
        {
            /*if (!IsValid(dto))
            {
                //throw new InvalidDataException();
            }*/
            if (await _entityDao.AnyAsync(EntityAlreadyExistsToCreateCondition(dto)))
            {
                //throw new EntityAlreadyExistsException();
            }
        }

        protected async virtual Task<TEntityDto> CreateActionsAsync(TEntity entity)
        {
            TEntity entityCreated = await _entityDao.CreateAsync(entity);
            return _mapper.Map<TEntityDto>(entityCreated);
        }

        protected async virtual Task<ICollection<TEntityDto>> CreateActionsAsync(ICollection<TEntity> entities)
        {
            ICollection<TEntity> created = await _entityDao.CreateAsync(entities);
            return _mapper.Map<ICollection<TEntityDto>>(created);
        }

        #endregion

        #region "Load"

        public async virtual Task<TEntityDto> LoadAsync(TAppId appId)
        {
            TEntity entity = await _entityDao.LoadAsync(appId);
            /*if ()
            {
                //throw new EntityDoesNotExistException();
            }*/

            return _mapper.Map<TEntityDto>(entity);
        }

        public async virtual Task<ICollection<TEntityDto>> LoadAllAsync()
        {
            ICollection<TEntity> entities = await _entityDao.LoadAllAsync();
            return entities.Select(l => _mapper.Map<TEntityDto>(l)).ToList();
        }

        #endregion

        #region "Update"

        public async virtual Task<TEntityDto> UpdateAsync(TEntityDto dto)
        {

            //TEntity entity = UpdateValidationActions(dto);
            //entity = _mapper.Map(dto, entity);
            TEntity entity = _mapper.Map<TEntity>(dto);
            return await UpdateActionsAsync(entity, dto);
        }

        protected virtual TEntity UpdateValidationActions(TEntityDto dto)
        {
            TEntity entity = _entityDao.Load(dto.Key());
            /*if (!IsValid(dto))
            {
                //throw new InvalidDataException();
            }*/
            /*if (await _entityDao.AnyAsync(EntityAlreadyExistsToUpdateCondition(dto)))
            {
                //throw new EntityAlreadyExistsException();
            }*/

            return entity;
        }

        protected async virtual Task<TEntityDto> UpdateActionsAsync(TEntity entity, TEntityDto dto)
        {
            TEntity updatedEntity = await _entityDao.UpdateAsync(entity);
            return _mapper.Map<TEntityDto>(updatedEntity);
        }

        #endregion

        #region "Delete"

        public async virtual Task DeleteAsync(TAppId appId)
        {
            await _entityDao.DeleteAsync(appId);
        }

        public async virtual Task DeleteAsync(ICollection<TAppId> listAppId)
        {
            //DeleteValidationActions(listAppId);
            await _entityDao.DeleteAsync(listAppId);
        }

        /*protected virtual DeleteValidationActions(ICollection<TAppId> listAppId)
        {
            /*if (!AreEntitiesVisible(listAppId, true))
            {
                //throw new EntityDoesNotExistException();
            }*/

        /*if (await _entityDao.AnyAsync(EntityHasDependenciesToDeleteCondition(listAppId)))
        {
            //throw new EntityHasDependenciesException();
        }
    }*/

        #endregion

        #region "Abstract methods"

        /// <summary>
        /// Returns if entity to create or update is valid
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract bool IsValid(TEntityDto dto);

        /// <summary>
        /// Condition to determine if an entity already exists
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract Expression<Func<TEntity, bool>> EntityAlreadyExistsCondition(TEntityDto dto);

        /// <summary>
        /// Condition to determine if entities of the list have dependencies with other entities and cannot be deleted
        /// </summary>
        /// <param name="listAppIds"></param>
        /// <returns></returns>
        protected abstract Expression<Func<TEntity, bool>> EntityHasDependenciesToDeleteCondition(ICollection<TAppId> listAppIds);

        #endregion

        #region "Protected methods"

        protected virtual Expression<Func<TEntity, bool>> EntityAlreadyExistsToCreateCondition(TEntityDto dto)
        {
            return EntityAlreadyExistsCondition(dto);
        }

        protected virtual Expression<Func<TEntity, bool>> EntityAlreadyExistsToUpdateCondition(TEntityDto dto)
        {
            return EntityAlreadyExistsCondition(dto);
        }
        #endregion
    }
}
