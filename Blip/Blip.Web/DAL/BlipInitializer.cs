using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Blip.Web.Models;

namespace Blip.Web.DAL
{
    public class BlipInitializer : System.Data.Entity.DropCreateDatabaseAlways<BlipContext>
    {
        protected override void Seed(BlipContext context)
        {
            var users = new List<User>
            {
                new User{UserName="admin",Password="adminpassword",Role="admin",Active=true,ActiveDate=DateTime.Parse("2015-01-01")},
                new User{UserName="user1",Password="user1password",Role="user",Active=true,ActiveDate=DateTime.Parse("2015-01-01")},
                new User{UserName="user2",Password="user2password",Role="user",Active=true,ActiveDate=DateTime.Parse("2015-01-01")},
                new User{UserName="user3",Password="user3password",Role="user",Active=true,ActiveDate=DateTime.Parse("2015-01-01")},
                new User{UserName="user4",Password="user4password",Role="user",Active=true,ActiveDate=DateTime.Parse("2015-01-01")},
                new User{UserName="user5",Password="user5password",Role="user",Active=true,ActiveDate=DateTime.Parse("2015-01-01")},
                new User{UserName="user6",Password="user6password",Role="user",Active=true,ActiveDate=DateTime.Parse("2015-01-01")},
                new User{UserName="user7",Password="user7password",Role="user",Active=true,ActiveDate=DateTime.Parse("2015-01-01")}
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var messages = new List<Message>
            {
                new Message{Title="Message 1",DateTime=DateTime.Parse("2015-01-01"),Body="Message 1 Blip, Blip, Blip"},
                new Message{Title="Message 2",DateTime=DateTime.Parse("2015-02-01"),Body="Message 2 Blip, Blip, Blip"},
                new Message{Title="Message 3",DateTime=DateTime.Parse("2015-03-01"),Body="Message 3 Blip, Blip, Blip"},
                new Message{Title="Message 4",DateTime=DateTime.Parse("2015-04-01"),Body="Message 4 Blip, Blip, Blip"},
                new Message{Title="Message 5",DateTime=DateTime.Parse("2015-05-01"),Body="Message 5 Blip, Blip, Blip"},
                new Message{Title="Message 6",DateTime=DateTime.Parse("2015-06-01"),Body="Message 6 Blip, Blip, Blip"},
                new Message{Title="Message 7",DateTime=DateTime.Parse("2015-07-01"),Body="Message 7 Blip, Blip, Blip"}
            };
            messages.ForEach(m => context.Messages.Add(m));
            context.SaveChanges();

            var messagePackets = new List<MessagePacket>
            {
                new MessagePacket{DateTime=DateTime.Parse("2015-08-01"),MessageID=1,SenderID=1,ReceiverID=2},
                new MessagePacket{DateTime=DateTime.Parse("2015-08-01"),MessageID=2,SenderID=2,ReceiverID=1},
                new MessagePacket{DateTime=DateTime.Parse("2015-08-01"),MessageID=3,SenderID=3,ReceiverID=1},
                new MessagePacket{DateTime=DateTime.Parse("2015-08-01"),MessageID=3,SenderID=3,ReceiverID=2},
                new MessagePacket{DateTime=DateTime.Parse("2015-08-01"),MessageID=4,SenderID=4,ReceiverID=7},
                new MessagePacket{DateTime=DateTime.Parse("2015-08-01"),MessageID=5,SenderID=5,ReceiverID=1},
                new MessagePacket{DateTime=DateTime.Parse("2015-08-01"),MessageID=5,SenderID=5,ReceiverID=7},
                new MessagePacket{DateTime=DateTime.Parse("2015-08-01"),MessageID=6,SenderID=6,ReceiverID=4},
                new MessagePacket{DateTime=DateTime.Parse("2015-08-01"),MessageID=7,SenderID=7,ReceiverID=2},
                new MessagePacket{DateTime=DateTime.Parse("2015-08-01"),MessageID=7,SenderID=7,ReceiverID=3},
                new MessagePacket{DateTime=DateTime.Parse("2015-08-01"),MessageID=7,SenderID=7,ReceiverID=5},
                new MessagePacket{DateTime=DateTime.Parse("2015-08-01"),MessageID=7,SenderID=7,ReceiverID=6}
            };
            messagePackets.ForEach(p => context.MessagePackets.Add(p));
            context.SaveChanges();
        }
    }
}