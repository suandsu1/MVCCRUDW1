using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCCRUDW1.Models
{   
	public  class View客戶統計表Repository : EFRepository<View客戶統計表>, IView客戶統計表Repository
	{

	}

	public  interface IView客戶統計表Repository : IRepository<View客戶統計表>
	{

	}
}