﻿using Microsoft.OpenApi.Models;

namespace Prokast.Server
{
    public static class OpenAPIConfig
    {
        public static void AddProkastOpenAPI(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, _) =>
                {
                    document.Info = new OpenApiInfo
                    {
                        Title = "Prokast API",
                        Version = "0.5",
                        Description = "API created for the Prokast project.",
                        Contact = new OpenApiContact
                        {
                            Name = "Twórca/Wsparcie Techniczne",
                            Email = "marekgrys01@gmail.com"
                        }
                    };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Additional Descriptions",
                        Description = "API focused on CRUDs for additional descriptions of products."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Additional Names",
                        Description = "API focused on CRUDs for additional names of products."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Clients",
                        Description = "API focused on (at least for now) registering clients."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Dictionary Parameters",
                        Description = "API focused on CRUDs for dictionary parameters that could be used for products."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Mailing",
                        Description = "API focused on mailing."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Orders",
                        Description = "API focused on CRUDs for orders of products."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Others",
                        Description = "A group of small endpoints not connected to any specific service or connected to all of them."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Custom Parameters",
                        Description = "API focused on CRUDs for custom parameters of products."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Photos",
                        Description = "API focused on CRUDs for photos of products."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Pricelists and prices",
                        Description = "API focused on CRUDs for pricelists and prices of products."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Products",
                        Description = "API focused on CRUDs for products."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "[Warehouse] Stored Products",
                        Description = "API focused on CRUDs for products stored in warehouses."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "[Warehouse] Warehouses",
                        Description = "API focused on CRUDs for warehouses."
                    }
                };

                    document.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Accounts",
                        Description = "API focused on accounts and logging in."
                    }
                };

                    return Task.CompletedTask;
                });
            });
        }
    }
}