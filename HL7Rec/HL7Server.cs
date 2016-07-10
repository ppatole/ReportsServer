
using System.ServiceProcess;
using System.Threading.Tasks;

namespace HL7Rec
{
    public partial class HL7Server : ServiceBase
    {
        public HL7Server()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Task t = new Task(() => HL7Listener.HL7Listener.HL7Listen());
            t.Start();
        }

        protected override void OnStop()
        {
            Task t = new Task(() => HL7Listener.HL7Listener.HL7StopListen());
            t.Start();
        }
    }
}
