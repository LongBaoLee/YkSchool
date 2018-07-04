/*
 * Created by SharpDevelop.
 * User: Albert Song
 * Date: 2008/01/04
 * Time: 17:21
 * 
 */

namespace Business
{
	/// <summary>
	/// Description of Page.
	/// </summary>
	public class Page
	{
		private int curPageIndex;
		private int totalRecord;
		private int pageSize;
		private object valueList;
		
		public Page()
		{
		}
		public Page(int totalRecord,int pageSize)
		{
			this.totalRecord=totalRecord;
			this.pageSize=pageSize;
		}
		public int CurPageIndex {
			get { return curPageIndex; }
			set { curPageIndex = value; }
		}
		
		public int TotalRecord {
			get { return totalRecord; }
			set { totalRecord = value; }
		}
		
		public int TotalPage {
			get { return (totalRecord+pageSize-1)/pageSize; }
		}
		
		public int PageSize {
			get { return pageSize; }
			set { pageSize = value; }
		}
		
		public object ValueList {
			get { return valueList; }
			set { valueList = value; }
		}
		
		
	}
}
