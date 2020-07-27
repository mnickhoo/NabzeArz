using NabzeArz.Service.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Areas.Admin.Models.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            this.totalTrackingCount = StateService.GetTrackingTotallCount(); 
            this.totalTrackingTodayCount = StateService.GetTrackingTodayCount();
            this.totalUsers = StateService.GetUserRegisterdCount(); 
        }
        public int totalTrackingCount { get; set; }
        public int totalTrackingTodayCount { get; set; }
        public int totalUsers { get; set; }
    }

}