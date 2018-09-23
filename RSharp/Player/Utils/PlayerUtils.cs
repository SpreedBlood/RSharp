using RSharp.API.Players;
using RSharp.API.Players.Skill;
using RSharp.API.Sessions;
using RSharp.Chat.Handles.Composers;
using RSharp.Interface.Handles.Composers;
using RSharp.Player.Handles.Composers;
using RSharp.World.Handles.Composers;
using System.Threading.Tasks;

namespace RSharp.Player.Utils
{
    public static class PlayerUtils
    {
        public static async Task PostLogin(this ISession session)
        {
            IPlayer player = session.Player;

            await session.WriteAndFlushAsync(new MapRegionComposer(player.Position));
            await session.WriteAndFlushAsync(new IsMemberComposer(player.Id, true));

            foreach (ISkill skill in player.Skills)
            {
                await session.WriteAndFlushAsync(new SkillLevelComposer(skill));
            }

            await session.WriteAndFlushAsync(new ResetScreenStateComposer());

            await session.WriteAndFlushAsync(new ChatOptionsComposer(0, 0, 0));

            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(1, 3917));
            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(2, 638));
            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(3, 3213));
            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(4, 1644));
            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(5, 5608));
            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(6, 1151)); //TODO: Different spellbooks, 1151 = default
            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(8, 5065));
            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(9, 5715));
            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(10, 2449));
            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(11, 904));
            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(12, 147));
            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(13, 962));
            await session.WriteAndFlushAsync(new SidebarInterfaceComposer(0, 2423));
        }
    }
}