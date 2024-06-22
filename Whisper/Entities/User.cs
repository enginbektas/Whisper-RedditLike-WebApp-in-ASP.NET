using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Whisper.Data.Repository;

namespace Whisper.Entities
{
    /// <summary>
    /// Veritabanını AspnetUsers tablosuna karşılık gelen model.
    /// İçerisinde property olmamasının sebebi Microsoft.AspNetCore.Identity kütüphanesinin IdentityUser class'ını inherit ediyor olmasından ötürüdür.
    /// IdentityUser class'ı içerisinde temel property'leri ( kolonları ) barındırmaktadır. Bu tabloya ekstra bir kolon eklenmek istendiği zaman
    /// User class'ı içerisine yeni property tanımlaması yapılması gerekir.
    /// </summary>
    /// 


    public class User : IdentityUser, IEntity
    {

        #region Navigation Properties
        public List<Community> MemberOfCommunities { get; set; }
        public List<Community> AdminOfCommunities { get; set; }
        public List<Community> ModOfCommunities { get; set; }
        public List<Thread> Threads { get; set; }
        public List<Comment> Comments { get; set; }
        
        #endregion


    }
}
