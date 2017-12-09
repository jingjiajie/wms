using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMS.DataAccess;

namespace WMS.UI
{
    public class ReceiptUtilities
    {
        public static SubmissionTicketItem ReceiptTicketItemToSubmissionTicketItem(ReceiptTicketItem receiptTicketItem, int submissionTicketID)
        {
            //WMSEntities wmsEntities = new WMSEntities();
            SubmissionTicketItem submissionTicketItem = new SubmissionTicketItem();
            //ReceiptTicketItemView receiptTicketItemView = (from rti in wmsEntities.ReceiptTicketItemView where rti.ID == receiptTicketItem.ID select rti).Single();
            submissionTicketItem.ComponentID = receiptTicketItem.ComponentID;
            submissionTicketItem.SubmissionTicketID = submissionTicketID;
            submissionTicketItem.ID = 0;
            submissionTicketItem.State = "送检中";
            submissionTicketItem.SubmissionAmount = receiptTicketItem.ReceiviptAmount;

            return submissionTicketItem;
        }
    }
}
