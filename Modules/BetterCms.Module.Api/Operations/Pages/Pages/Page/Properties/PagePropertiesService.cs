﻿using System;
using System.Collections.Generic;
using System.Linq;

using BetterCms.Core.DataAccess;
using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.DataContracts.Enums;
using BetterCms.Module.Api.Helpers;
using BetterCms.Module.Api.Operations.Root;
using BetterCms.Module.MediaManager.Services;
using BetterCms.Module.Pages.Models;
using BetterCms.Module.Pages.Services;
using BetterCms.Module.Root.Models;
using BetterCms.Module.Root.Mvc.Helpers;
using BetterCms.Module.Root.Services;

using ServiceStack.ServiceInterface;

using AccessLevel = BetterCms.Module.Api.Operations.Root.AccessLevel;

namespace BetterCms.Module.Api.Operations.Pages.Pages.Page.Properties
{
    /// <summary>
    /// Default page properties CRUD service.
    /// </summary>
    public class PagePropertiesService : Service, IPagePropertiesService
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository repository;

        /// <summary>
        /// The URL service.
        /// </summary>
        private readonly IUrlService urlService;

        /// <summary>
        /// The option service.
        /// </summary>
        private readonly IOptionService optionService;

        /// <summary>
        /// The file URL resolver.
        /// </summary>
        private readonly IMediaFileUrlResolver fileUrlResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagePropertiesService" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="urlService">The URL service.</param>
        /// <param name="optionService">The option service.</param>
        /// <param name="fileUrlResolver">The file URL resolver.</param>
        public PagePropertiesService(
            IRepository repository,
            IUrlService urlService,
            IOptionService optionService,
            IMediaFileUrlResolver fileUrlResolver)
        {
            this.repository = repository;
            this.urlService = urlService;
            this.optionService = optionService;
            this.fileUrlResolver = fileUrlResolver;
        }

        /// <summary>
        /// Gets the specified page properties.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   <c>GetPagePropertiesResponse</c> with a page properties data.
        /// </returns>
        public GetPagePropertiesResponse Get(GetPagePropertiesRequest request)
        {
            var query = repository.AsQueryable<PageProperties>();

            if (request.PageId.HasValue)
            {
                query = query.Where(page => page.Id == request.PageId.Value);
            }
            else
            {
                var url = urlService.FixUrl(request.PageUrl);
                query = query.Where(page => page.PageUrlHash == url.UrlHash());
            }

            var response = query
                .Select(page => new GetPagePropertiesResponse
                    {
                        Data = new PagePropertiesModel
                            {
                                Id = page.Id,
                                Version = page.Version,
                                CreatedBy = page.CreatedByUser,
                                CreatedOn = page.CreatedOn,
                                LastModifiedBy = page.ModifiedByUser,
                                LastModifiedOn = page.ModifiedOn,

                                PageUrl = page.PageUrl,
                                Title = page.Title,
                                Description = page.Description,
                                IsPublished = page.Status == PageStatus.Published,
                                PublishedOn = page.PublishedOn,
                                LayoutId = page.Layout != null && !page.Layout.IsDeleted ? page.Layout.Id : (Guid?)null,
                                MasterPageId = page.MasterPage != null && !page.MasterPage.IsDeleted ? page.MasterPage.Id : (Guid?)null,
                                CategoryId = page.Category != null && !page.Category.IsDeleted ? page.Category.Id : (Guid?)null,
                                IsArchived = page.IsArchived,
                                IsMasterPage = page.IsMasterPage,
                                LanguageGroupIdentifier = page.LanguageGroupIdentifier,
                                LanguageId = page.Language != null ? page.Language.Id : (Guid?)null,
                                MainImageId = page.Image != null && !page.Image.IsDeleted ? page.Image.Id : (Guid?)null,
                                FeaturedImageId = page.FeaturedImage != null && !page.FeaturedImage.IsDeleted ? page.FeaturedImage.Id : (Guid?)null,
                                SecondaryImageId = page.SecondaryImage != null && !page.SecondaryImage.IsDeleted ? page.SecondaryImage.Id : (Guid?)null,
                                CustomCss = page.CustomCss,
                                CustomJavaScript = page.CustomJS,
                                UseCanonicalUrl = page.UseCanonicalUrl,
                                UseNoFollow = page.UseNoFollow,
                                UseNoIndex = page.UseNoIndex
                            },
                        MetaData = request.Data.IncludeMetaData 
                            ? new MetadataModel
                            {
                                MetaTitle = page.MetaTitle,
                                MetaDescription = page.MetaDescription,
                                MetaKeywords = page.MetaKeywords
                            } 
                            : null,
                        Category = page.Category != null && !page.Category.IsDeleted && request.Data.IncludeCategory 
                            ? new CategoryModel
                            {
                                Id = page.Category.Id,
                                Version = page.Category.Version,
                                CreatedBy = page.Category.CreatedByUser,
                                CreatedOn = page.Category.CreatedOn,
                                LastModifiedBy = page.Category.ModifiedByUser,
                                LastModifiedOn = page.Category.ModifiedOn,
                                Name = page.Category.Name
                            }
                            : null,
                        Layout = request.Data.IncludeLayout && !page.Layout.IsDeleted 
                            ? new LayoutModel
                            {
                                Id = page.Layout.Id,
                                Version = page.Layout.Version,
                                CreatedBy = page.Layout.CreatedByUser,
                                CreatedOn = page.Layout.CreatedOn,
                                LastModifiedBy = page.Layout.ModifiedByUser,
                                LastModifiedOn = page.Layout.ModifiedOn,

                                Name = page.Layout.Name,
                                LayoutPath = page.Layout.LayoutPath,
                                PreviewUrl = page.Layout.PreviewUrl
                            } 
                            : null,
                        MainImage = page.Image != null && !page.Image.IsDeleted && request.Data.IncludeImages 
                            ? new ImageModel
                            {
                                Id = page.Image.Id,
                                Version = page.Image.Version,
                                CreatedBy = page.Image.CreatedByUser,
                                CreatedOn = page.Image.CreatedOn,
                                LastModifiedBy = page.Image.ModifiedByUser,
                                LastModifiedOn = page.Image.ModifiedOn,

                                Title = page.Image.Title,
                                Caption = page.Image.Caption,
                                Url = fileUrlResolver.EnsureFullPathUrl(page.Image.PublicUrl),
                                ThumbnailUrl = fileUrlResolver.EnsureFullPathUrl(page.Image.PublicThumbnailUrl)
                            } 
                            : null,
                        FeaturedImage = page.FeaturedImage != null && !page.FeaturedImage.IsDeleted && request.Data.IncludeImages 
                            ? new ImageModel
                            {
                                Id = page.FeaturedImage.Id,
                                Version = page.FeaturedImage.Version,
                                CreatedBy = page.FeaturedImage.CreatedByUser,
                                CreatedOn = page.FeaturedImage.CreatedOn,
                                LastModifiedBy = page.FeaturedImage.ModifiedByUser,
                                LastModifiedOn = page.FeaturedImage.ModifiedOn,

                                Title = page.FeaturedImage.Title,
                                Caption = page.FeaturedImage.Caption,
                                Url = fileUrlResolver.EnsureFullPathUrl(page.FeaturedImage.PublicUrl),
                                ThumbnailUrl = fileUrlResolver.EnsureFullPathUrl(page.FeaturedImage.PublicThumbnailUrl)
                            } 
                            : null,
                        SecondaryImage = page.SecondaryImage != null && !page.SecondaryImage.IsDeleted && request.Data.IncludeImages 
                            ? new ImageModel
                            {
                                Id = page.SecondaryImage.Id,
                                Version = page.SecondaryImage.Version,
                                CreatedBy = page.SecondaryImage.CreatedByUser,
                                CreatedOn = page.SecondaryImage.CreatedOn,
                                LastModifiedBy = page.SecondaryImage.ModifiedByUser,
                                LastModifiedOn = page.SecondaryImage.ModifiedOn,

                                Title = page.SecondaryImage.Title,
                                Caption = page.SecondaryImage.Caption,
                                Url = fileUrlResolver.EnsureFullPathUrl(page.SecondaryImage.PublicUrl),
                                ThumbnailUrl = fileUrlResolver.EnsureFullPathUrl(page.SecondaryImage.PublicThumbnailUrl)
                            } 
                            : null,
                        Language = page.Language != null && !page.Language.IsDeleted && request.Data.IncludeLanguage
                            ? new LanguageModel
                            {
                                Id = page.Language.Id,
                                Version = page.Language.Version,
                                CreatedBy = page.Language.CreatedByUser,
                                CreatedOn = page.Language.CreatedOn,
                                LastModifiedBy = page.Language.ModifiedByUser,
                                LastModifiedOn = page.Language.ModifiedOn,

                                Name = page.Language.Name,
                                Code = page.Language.Code,
                            }
                            : null,
                    })
                .FirstOne();

            if (request.Data.IncludeTags)
            {
                response.Tags = LoadTags(response.Data.Id);
            }

            if (request.Data.IncludePageContents)
            {
                response.PageContents = LoadPageContents(response.Data.Id);
            }

            if (request.Data.IncludePageOptions)
            {
                // Get layout options, page options and merge them
                response.PageOptions = optionService
                    .GetMergedMasterPagesOptionValues(response.Data.Id, response.Data.MasterPageId, response.Data.LayoutId)
                    .Select(o => new OptionValueModel
                        {
                            Key = o.OptionKey,
                            Value = o.OptionValue,
                            DefaultValue = o.OptionDefaultValue,
                            Type = (Root.OptionType)(int)o.Type
                        })
                    .ToList();
            }
            
            if (request.Data.IncludeAccessRules)
            {
                response.AccessRules = LoadAccessRules(response.Data.Id);
            }
            
            if (request.Data.IncludePageTranslations 
                && response.Data.LanguageGroupIdentifier.HasValue)
            {
                response.PageTranslations = repository
                    .AsQueryable<PageProperties>()
                    .Where(p => p.LanguageGroupIdentifier == response.Data.LanguageGroupIdentifier)
                    .OrderBy(p => p.Title)
                    .Select(p => new PageTranslationModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                            PageUrl = p.PageUrl,
                            LanguageId = p.Language != null ? p.Language.Id : (Guid?)null,
                            LanguageCode = p.Language != null ? p.Language.Code : null,
                        })
                    .ToList();
            }

            return response;
        }

        /// <summary>
        /// Loads the access rules.
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>Page access rules collection.</returns>
        private List<AccessRuleModel> LoadAccessRules(Guid pageId)
        {
            return (from page in repository.AsQueryable<Module.Root.Models.Page>()
                    from accessRule in page.AccessRules
                    where page.Id == pageId
                    orderby accessRule.IsForRole, accessRule.Identity
                    select new AccessRuleModel
                    {
                        AccessLevel = (AccessLevel)(int)accessRule.AccessLevel,
                        Identity = accessRule.Identity,
                        IsForRole = accessRule.IsForRole
                    })
                    .ToList();
        }

        /// <summary>
        /// Loads the tags.
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>Page tags collection.</returns>
        private List<TagModel> LoadTags(Guid pageId)
        {
            return repository
                .AsQueryable<PageTag>(pageTag => pageTag.Page.Id == pageId && !pageTag.Tag.IsDeleted)
                .Select(media => new TagModel
                {
                    Id = media.Tag.Id,
                    Version = media.Tag.Version,
                    CreatedBy = media.Tag.CreatedByUser,
                    CreatedOn = media.Tag.CreatedOn,
                    LastModifiedBy = media.Tag.ModifiedByUser,
                    LastModifiedOn = media.Tag.ModifiedOn,

                    Name = media.Tag.Name
                }).ToList();
        }

        /// <summary>
        /// Loads the page contents.
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>Page contents collection.</returns>
        private List<PageContentModel> LoadPageContents(Guid pageId)
        {
            var results = repository
                 .AsQueryable<PageContent>(pageContent => pageContent.Page.Id == pageId && !pageContent.Content.IsDeleted)
                 .OrderBy(pageContent => pageContent.Order)
                 .Select(pageContent => new
                    {
                        Type = pageContent.Content.GetType(),
                        Model = new PageContentModel
                            {
                                Id = pageContent.Id,
                                Version = pageContent.Version,
                                CreatedBy = pageContent.CreatedByUser,
                                CreatedOn = pageContent.CreatedOn,
                                LastModifiedBy = pageContent.ModifiedByUser,
                                LastModifiedOn = pageContent.ModifiedOn,

                                ContentId = pageContent.Content.Id,
                                Name = pageContent.Content.Name,
                                RegionId = pageContent.Region.Id,
                                RegionIdentifier = pageContent.Region.RegionIdentifier,
                                Order = pageContent.Order,
                                IsPublished = pageContent.Content.Status == ContentStatus.Published
                            }
                    }).ToList();

            // Set content types
            results.ToList().ForEach(item => item.Model.ContentType = item.Type.ToContentTypeString());

            return results.Select(item => item.Model).ToList();
        }
    }
}