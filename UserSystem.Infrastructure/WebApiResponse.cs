using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.Infrastructure
{

    /// <summary>
    /// 返回统一结果
    /// </summary>
    /// <typeparam name="T">T是返回结果的类型</typeparam>
  public  class WebApiResponse<T>
    {
        /// <summary>
        /// 结果
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public WebApiStateCode ErrCode { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { get; set; }
    }
}
