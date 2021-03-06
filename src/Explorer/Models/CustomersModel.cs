﻿// -----------------------------------------------------------------------
// <copyright file="CustomersModel.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Explorer.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Model for listing existing customers.
    /// </summary>
    public class CustomersModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerModel" /> class.
        /// </summary>
        public CustomersModel()
        {
            Customers = new List<CustomerModel>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerModel" /> class.
        /// </summary>
        /// <param name="items">Items to be added to the customers collection.</param>
        public CustomersModel(IList<CustomerModel> items)
        {
            Customers = new List<CustomerModel>(items);
        }


        /// <summary>
        /// Gets a collection of customer that belong to the configured partner.
        /// </summary>
        public List<CustomerModel> Customers { get; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the configured tenant is the integration sandbox.
        /// </summary>
        public bool IsSandboxEnvironment { get; set; }
    }
}