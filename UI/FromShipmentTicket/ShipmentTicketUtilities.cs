using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMS.DataAccess;

namespace WMS.UI
{
    class ShipmentTicketUtilities
    {
        public static void UpdateShipmentTicketStateSync(int shipmentTicketID)
        {
            WMSEntities wmsEntities = new WMSEntities();
            int total = wmsEntities.Database.SqlQuery<int>(string.Format(
                @"SELECT COUNT(*) FROM ShipmentTicketItem 
                    WHERE ShipmentTicketID = {0}",
                shipmentTicketID
                )).Single();
            int fullAssignedCount = wmsEntities.Database.SqlQuery<int>(string.Format(
                @"SELECT COUNT(*) FROM ShipmentTicketItem 
                    WHERE ShipmentTicketID = {0} AND ScheduledJobAmount = ShipmentAmount",
                shipmentTicketID
                )).Single();
            int notAssignedCount = wmsEntities.Database.SqlQuery<int>(string.Format(
                @"SELECT COUNT(*) FROM ShipmentTicketItem 
                    WHERE ShipmentTicketID = {0} AND ScheduledJobAmount = 0",
                shipmentTicketID
                )).Single();
            if (notAssignedCount == total)
            {
                wmsEntities.Database.ExecuteSqlCommand(string.Format(
                    @"UPDATE ShipmentTicket SET State = '{0}' WHERE ID = {1}", ShipmentTicketViewMetaData.STRING_STATE_NOT_ASSIGNED_JOB, shipmentTicketID));
            }
            else if (fullAssignedCount == total)
            {
                wmsEntities.Database.ExecuteSqlCommand(string.Format(
                    @"UPDATE ShipmentTicket SET State = '{0}' WHERE ID = {1}", ShipmentTicketViewMetaData.STRING_STATE_ALL_ASSIGNED_JOB, shipmentTicketID));
            }
            else
            {
                wmsEntities.Database.ExecuteSqlCommand(string.Format(
                    @"UPDATE ShipmentTicket SET State = '{0}' WHERE ID = {1}", ShipmentTicketViewMetaData.STRING_STATE_PART_ASSIGNED_JOB, shipmentTicketID));
            }
            wmsEntities.SaveChanges();
        }
    }
}
