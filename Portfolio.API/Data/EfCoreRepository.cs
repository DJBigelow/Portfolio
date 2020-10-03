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

        public IEnumerable<Project> Projects => context.Projects;

        public EfCoreRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }



        public IEnumerable<Project> GetProjects() => context.Projects;

        public async Task<Project> GetProjectAsync(int projectID)
        {
            var project = await context.Projects
                .Include(p => p.ProjectFrameworks).ThenInclude(pf => pf.Framework)
                .Include(p => p.ProjectLanguages).ThenInclude(pl => pl.Language)
                .Include(p => p.ProjectPlatforms).ThenInclude(pp => pp.Platform)
                .FirstOrDefaultAsync(p => p.ID == projectID);
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
            //Instead of simply calling context.Update(project) from the get-go, we  have to manually copy the data from project to existingProject because they aren't the same object. 
            //This is because project is a new Project object converted from a ProjectViewModel object received from the front-end
            var existingProject = await GetProjectAsync(project.ID);

            existingProject.Title = project.Title;
            existingProject.Requirement = project.Requirement;
            existingProject.Design = project.Design;
            existingProject.CompletionDate = project.CompletionDate;

            context.Update(existingProject);
            await context.SaveChangesAsync();
        }



        public async Task AssociateProjectAndFramework(int projectID, string frameworkName)
        {
            Framework framework = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(context.Frameworks, f => f.Name == frameworkName);

            if (framework == null)
            {
                framework = new Framework() { Name = frameworkName };
                context.Frameworks.Add(framework);
                await context.SaveChangesAsync();           
            }

            Project project = await GetProjectAsync(projectID);
            ProjectFramework pf = new ProjectFramework() { Framework = framework, Project = project };

            context.ProjectFrameworks.Add(pf);
            await context.SaveChangesAsync();
        }

        public async Task AssociateProjectAndLanguage(int projectID, string languageName)
        {
            Language language = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(context.Languages, f => f.Name == languageName);

            if (language == null)
            {
                language = new Language() { Name = languageName };
                context.Languages.Add(language);
                await context.SaveChangesAsync();
            }

            Project project = await GetProjectAsync(projectID);
            ProjectLanguage pl = new ProjectLanguage() { Language = language, Project = project };

            context.ProjectLanguages.Add(pl);
            await context.SaveChangesAsync();

        }

        public async Task AssociateProjectAndPlatform(int projectID, string platformName)
        {
            Platform platform = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(context.Platforms, f => f.Name == platformName);

            if (platform == null)
            {
                platform = new Platform() { Name = platformName };
                context.Platforms.Add(platform);
                await context.SaveChangesAsync();
            }

            Project project = await GetProjectAsync(projectID);
            ProjectPlatform pp = new ProjectPlatform() { Platform = platform, Project = project };

            context.ProjectPlatforms.Add(pp);
            await context.SaveChangesAsync();
        }


        //public async Task Associate(AssociationRequest associationRequest)
        //{
        //    switch(associationRequest.CategoryType)
        //    {
        //        case Categories.FRAMEWORK:
        //            Framework framework = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(context.Frameworks, f => f.Name == associationRequest.Category);

        //            if (framework == null)
        //            {
        //                framework = new Framework() { Name = associationRequest.Category };
        //                context.Frameworks.Add(framework);
        //                await context.SaveChangesAsync();
        //            }

        //            Project project = await GetProjectAsync(associationRequest.ProjectID);
        //            ProjectFramework pf = new ProjectFramework() { Framework = framework, Project = project };

        //            context.ProjectFrameworks.Add(pf);
        //            await context.SaveChangesAsync();
        //            break;

        //        case Categories.LANGUAGE:
        //            break;

        //        case Categories.PLATFORM:
        //            break;
        //    }
        //}
    }
}
