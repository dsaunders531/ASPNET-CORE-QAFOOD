﻿using mezzanine;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.BLL.Models
{
    /// <summary>
    /// The page meta model for defining the page meta tags using the meta tag helper.
    /// </summary>
    [NotMapped]
    public sealed class PageMetaModel : PageMeta
    {
        public PageMetaModel()
        {
            // Set the defaults
            this.AddHtml5Defaults = true;
            this.AppInfo = new mezzanine.Utility.AssemblyInfo();            
            this.RobotsFollow = true;
            this.RobotsIndex = true;
        }
    }
}
