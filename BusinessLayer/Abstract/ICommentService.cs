using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
        List<Comment> TGetDestinationById(int id);    //herzaman gerekli değil ama ikisini implement edildi
        List<Comment> TGetListCommentWithDestination();  //Comment üzerinden verileri çekmek için 
        public List<Comment> TGetListCommentWithDestinationAndUser(int id);
    }
}
