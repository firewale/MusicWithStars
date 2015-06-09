using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MusicServices.RatingService
{
    public partial class RatingService : ServiceBase
    {
        private BootStrapper _bootStrapper;

        public RatingService()
        {
            InitializeComponent();
            _bootStrapper = new BootStrapper();
        }

        protected override void OnStart(string[] args)
        {
            _bootStrapper.Start();
        }

        protected override void OnStop()
        {
            _bootStrapper.Stop();
        }
    }
}
