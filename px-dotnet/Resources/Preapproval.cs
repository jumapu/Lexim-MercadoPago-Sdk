using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MercadoPago.Common;
using MercadoPago.DataStructures.Preapproval;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MercadoPago.Resources
{
    public sealed partial class Preapproval : Resource<Preapproval>
    {
        #region Actions

        /// <summary>
        /// Get all preapprovals acoording to specific filters
        /// </summary>
        public static List<Preapproval> Search(Dictionary<string, string> filters, bool useCache = false, string accessToken = null) =>
            GetList("/preapproval/search", accessToken, useCache, filters);

        public static IQueryable<Preapproval> Query(bool useCache = false, string accessToken = null) =>
            CreateQuery("/preapproval/search", accessToken, useCache);

        /// <summary>
        /// Find a preapproval trought an unique identifier with Local Cache Flag
        /// </summary>
        public static Preapproval FindById(string id, bool useCache = false, string accessToken = null) => 
            Get($"/preapproval/{id}", accessToken, useCache);

        /// <summary>
        /// Save a new preapproval
        /// </summary>
        public Preapproval Save() => Post("/preapproval");

        /// <summary>
        ///  Update editable properties
        /// </summary>
        public Preapproval Update() => Put($"/preapproval/{Id}");

        #endregion

        #region Properties

        /// <summary>
        ///  Identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///  Buy reason
        /// </summary>
        public string Reason { get; set; }        

        /// <summary>
        ///  Payer Email
        /// </summary>
        [StringLength(256)]
        public string PayerEmail { get; set; }

        /// <summary>
        ///  Return  URL
        /// </summary>
        [StringLength(500)]
        public string BackUrl { get; set; }

        /// <summary>
        /// Checkout access URL
        /// </summary>
        public string InitPoint { get; private set; }

        /// <summary>
        /// Sandbox checkout access URL
        /// </summary>
        public string SandboxInitPoint { get; set; }

        /// <summary>
        /// Auro Recurring Information
        /// </summary>
        public AutoRecurring? AutoRecurring { get; set; }

        /// <summary>
        /// Reference you can synchronize with your payment system
        /// </summary>
        [StringLength(256)]
        public string ExternalReference { get; set; }

        /// <summary>
        /// Preapproval's status
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PreapprovalStatus? Status { get; set; }

        /// <summary>
        /// Preapproval's creation date
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// Preapproval's last modified date
        /// </summary>
        public DateTime LastModified { get; private set; }

        #endregion
    }
}