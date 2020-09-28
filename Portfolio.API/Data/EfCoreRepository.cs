﻿using Microsoft.EntityFrameworkCore;
using Portfolio.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Shared.Data
{
    public class EfCoreRepository : IRepository
    {
        private readonly ApplicationDbContext context;

        public EfCoreRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        

        public IEnumerable<Project> GetProjects()
        {
            return context.Projects;
        }

        public async Task<Project> GetProjectAsync(int projectID)
        {
            var project = await context.Projects.FirstOrDefaultAsync(p => p.ID == projectID);
            //return await context.Projects.FirstOrDefaultAsync(p => p.ID == projectID);
            return project;
        }

        public async Task AddProjectAsync(Project project)
        {
            await context.AddAsync(project);
            await context.SaveChangesAsync();
        }
        public async Task DeleteProjectAsync(int projectID)
        {            

            context.Remove(await GetProjectAsync(projectID));
            await context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            context.Update(project);
            await context.SaveChangesAsync();
        }


   
    }
}