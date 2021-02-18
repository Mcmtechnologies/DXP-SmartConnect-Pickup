using System;

namespace DXP.SmartConnectPickup.Common.Models
{
    public class BasePagingResultModel : IPagingResult
    {
        private int _totalPages;

        public int TotalPages
        {
            get
            {
                if (_totalPages == 0)
                {
                    _totalPages = CalculateTotalPages();
                }

                return _totalPages;
            }
            set { _totalPages = value; }
        }

        /// <summary>
        /// Total Record to return
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Page index
        /// </summary>
        public int PageIndex { get; set; }

        private int CalculateTotalPages()
        {
            if (PageSize == 0 || TotalRecords == 0)
            {
                return 0;
            }

            return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(TotalRecords) / PageSize));
        }
    }

    public interface IPagingResult
    {
        int TotalPages { get; set; }
        int TotalRecords { get; set; }
        int PageSize { get; set; }
        int PageIndex { get; set; }
    }
}
