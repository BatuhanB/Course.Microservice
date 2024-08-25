// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Course.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={ "catalog_fullpermission" } },
            new ApiResource("resource_photostock"){Scopes={ "photostock_fullpermission" } },
            new ApiResource("resource_basket"){Scopes={ "basket_fullpermission" } },
            new ApiResource("resource_discount"){Scopes={ "discount_fullpermission" } },
            new ApiResource("resource_order"){Scopes={ "order_fullpermission" } },
            new ApiResource("resource_fake_payment"){Scopes={ "fake_payment_fullpermission" } },
            new ApiResource("resource_gateway"){Scopes={ "gateway_fullpermission" } },
            new ApiResource("resource_notification"){Scopes={ "notification_fullpermission" } },
            new ApiResource("resource_invoice"){Scopes={ "invoice_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){
                           Name="roles",
                           DisplayName="Roles",
                           Description="User Roles",
                           UserClaims= new[]{ "role" }
                       },

                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","Full Permission For Catalog API"),
                new ApiScope("photostock_fullpermission","Full Permission For Photo Stock API"),
                new ApiScope("basket_fullpermission","Full Permission For Basket API"),
                new ApiScope("discount_fullpermission","Full Permission For Discount API"),
                new ApiScope("order_fullpermission","Full Permission For Order API"),
                new ApiScope("fake_payment_fullpermission","Full Permission For Fake Payment API"),
                new ApiScope("gateway_fullpermission","Full Permission For API Gateway"),
                new ApiScope("notification_fullpermission","Full Permission For Notification API"),
                new ApiScope("invoice_fullpermission","Full Permission For Invoice API"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientName="ASP.Net Core MVC",
                    ClientId="FrontEndClient",
                    ClientSecrets={ new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes={ "catalog_fullpermission",
                                    "photostock_fullpermission",
                                    "gateway_fullpermission",
                                    IdentityServerConstants.LocalApi.ScopeName}
                },
                new Client()
                {
                    ClientName="ASP.Net Core MVC",
                    ClientId="FrontEndClientWithResource",
                    AllowOfflineAccess=true,
                    ClientSecrets={ new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={ "basket_fullpermission",
                                    "order_fullpermission",
                                    "gateway_fullpermission",
                                    "notification_fullpermission",
                                    IdentityServerConstants.LocalApi.ScopeName,
                                    IdentityServerConstants.StandardScopes.Email,
                                    IdentityServerConstants.StandardScopes.OpenId,
                                    IdentityServerConstants.StandardScopes.Profile,
                                    IdentityServerConstants.StandardScopes.OfflineAccess},
                    AccessTokenLifetime = 3600,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                },
                new Client()
                {
                    ClientName="Token Exchange Client",
                    ClientId="TokenExchangeClient",
                    ClientSecrets={ new Secret("secret".Sha256())},
                    AllowedGrantTypes= new [] {"urn:ietf:params:oauth:grant-type:token-exchange" },
                    AllowedScopes={
                        "discount_fullpermission",
                        "fake_payment_fullpermission",
                        "invoice_fullpermission",
                        IdentityServerConstants.StandardScopes.OpenId }
                },

            };
    }
}