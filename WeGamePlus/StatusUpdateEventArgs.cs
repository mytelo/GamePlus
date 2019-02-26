using System;

namespace WeGamePlus
{
    public class StatusUpdateEventArgs : EventArgs
    {
        public StatusUpdateEventArgs(int got, int total)
        {
            this.BytesGot = got;
            this.BytesTotal = total;
        }

        public int BytesGot { get; }

        public int BytesTotal { get; }
    }
}

