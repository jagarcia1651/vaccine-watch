using System;
using System.Collections.Generic;

namespace vaccine_watch
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class MN
    {
        public string city { get; set; }
        public string state { get; set; }
        public string status { get; set; }
    }

    public class Data
    {
        public List<MN> MN { get; set; }
    }

    public class ResponsePayloadData
    {
        public DateTime currentTime { get; set; }
        public Data data { get; set; }
        public bool isBookingCompleted { get; set; }
    }

    public class ResponseMetaData
    {
        public string statusDesc { get; set; }
        public string conversationId { get; set; }
        public string refId { get; set; }
        public string operation { get; set; }
        public string version { get; set; }
        public string statusCode { get; set; }
    }

    public class Root
    {
        public ResponsePayloadData responsePayloadData { get; set; }
        public ResponseMetaData responseMetaData { get; set; }
    }


}