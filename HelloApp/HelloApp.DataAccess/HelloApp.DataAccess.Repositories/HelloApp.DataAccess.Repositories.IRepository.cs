﻿public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Remove(T entity);
    void Update(T entity);
}
