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

        public static bool DeleteItemsSync(int[] itemIDs,out string errorMessage)
        {
            try
            {
                using (WMSEntities wmsEntities = new WMSEntities())
                {
                    foreach (int id in itemIDs)
                    {
                        ShipmentTicketItem item = (from s in wmsEntities.ShipmentTicketItem where s.ID == id select s).FirstOrDefault();
                        if (item == null) continue;
                        if (item.ScheduledJobAmount > 0)
                        {
                            errorMessage = "不能删除已分配翻包的零件！";
                            return false;
                        }

                        //把库存已分配发货数减回去
                        decimal? amount = item.ShipmentAmount * item.UnitAmount;
                        StockInfo stockInfo = (from s in wmsEntities.StockInfo where s.ID == item.StockInfoID select s).FirstOrDefault();
                        if (stockInfo == null) continue;
                        stockInfo.ScheduledShipmentAmount -= amount ?? 0;
                        wmsEntities.ShipmentTicketItem.Remove(item);
                    }
                    wmsEntities.SaveChanges();
                    errorMessage = null;
                    return true;
                }
            }
            catch
            {
                errorMessage = "操作失败，请检查网络连接";
                return false;
            }
        }
    }
}
