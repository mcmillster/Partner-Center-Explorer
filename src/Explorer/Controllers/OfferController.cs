﻿// -----------------------------------------------------------------------
// <copyright file="OfferController.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Explorer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Logic;
    using Models;
    using PartnerCenter.Models.Offers;
    using Providers;

    /// <summary>
    /// Processes offer related operations.
    /// </summary>
    [RoutePrefix("offers")]
    public class OfferController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferController"/> class.
        /// </summary>
        /// <param name="service">Provides access to core services.</param>
        public OfferController(IExplorerProvider provider) : base(provider)
        {
        }

        /// <summary>
        /// Handles the Offers partial view request.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>The HTML template for the offers view.</returns>
        [HttpGet]
        [Route("")]
        public async Task<PartialViewResult> GetOffersAsync(string customerId)
        {
            List<Offer> offers;
            List<OfferModel> models;
            OffersModel offersModel;

            try
            {
                offers = await Provider.PartnerOperations.GetOffersAsync().ConfigureAwait(false);

                offersModel = new OffersModel()
                {
                    CustomerId = customerId
                };

                models = (from offer in offers
                          where offer.IsAvailableForPurchase
                          select new OfferModel()
                          {
                              Billing = offer.Billing,
                              Description = offer.Description,
                              Id = offer.Id,
                              IsAddOn = offer.IsAddOn,
                              IsAvailableForPurchase = offer.IsAvailableForPurchase,
                              MaximumQuantity = offer.MaximumQuantity,
                              MinimumQuantity = offer.MinimumQuantity,
                              Name = offer.Name,
                              PrerequisiteOffers = offer.PrerequisiteOffers
                          }).ToList();

                offersModel.AvailableOffers.AddRange(models);

                return PartialView("Offers", offersModel);
            }
            finally
            {
                offers = null;
            }
        }

        /// <summary>
        /// Gets the specified offer.
        /// </summary>
        /// <param name="offerId">Identifier for the offer.</param>
        /// <returns>A specific offer in JSON.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="offerId"/> is empty or null.
        /// </exception>
        [HttpGet]
        [Route("{offerId}")]
        public async Task<JsonResult> GetOfferAsync(string offerId)
        {
            List<Offer> offers;

            offerId.AssertNotEmpty(nameof(offerId));

            try
            {
                offers = await Provider.PartnerOperations.GetOffersAsync().ConfigureAwait(false);

                return Json(
                    offers.Single(x => x.Id.Equals(offerId, StringComparison.CurrentCultureIgnoreCase)),
                    JsonRequestBehavior.AllowGet);
            }
            finally
            {
                offers = null;
            }
        }
    }
}