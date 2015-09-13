using System;

namespace CarManage.Model
{
    [Serializable]
    public class BaseModel
    {
        public BaseModel()
        {
            PageIndex = 0;
            PageSize = 20;
            TotalCount = 0;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize.Equals(0))
                    return 0;

                if (TotalCount % PageSize == 0)
                    return TotalCount / PageSize;

                return TotalCount / PageSize + 1;
            }
        }
    }
}
