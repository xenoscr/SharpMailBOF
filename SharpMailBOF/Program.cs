using System;

namespace BOFNET.Bofs
{
    public class SharpMailBof : BeaconObject
    {
        public SharpMailBof(BeaconApi api) : base(api) { }
        public override void Go(string[] args)
        {
            try
            {
                string mailResults = SharpMail.SharpMailSend.Send(args);
                BeaconConsole.Write(mailResults);
            }
            catch (Exception e)
            {
                BeaconConsole.WriteLine($"Unhandled terminating exception: {e}");
            }
        }
    }
}
