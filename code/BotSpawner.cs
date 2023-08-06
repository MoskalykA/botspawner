using Sandbox.Tools;
using System;
using System.Collections.Generic;

namespace Sandbox.MoskalykA
{
	[Library( "tool_botspawner", Title = "Bot spawner", Description = "Left click: Spawn a bot at the spawn site\nRight click: Spawn a bot in front of you\nReload: Kick all the bots", Group = "fun" )]
	public class BotSpawner : BaseTool
	{
        Random Random = new Random();
        
		List<String> Names = new List<String>
		{
			"Luc DeGarmo",
			"Kevin Montagne",
			"Gautier Gauthier",
			"Lucas Étienne",
			"Sohan Bellerose"
		};
		
		List<Sandbox.Bot> Bots = new List<Sandbox.Bot>();
		
		public override void Simulate()
		{
			if ( !Game.IsServer )
				return;

			if (Input.Pressed( "attack1" ))
			{
				var Name = Names[Random.Next( Names.Count )];
				Bots.Add( new Sandbox.Bot( Name ) );
			} else if ( Input.Pressed( "attack2" ) )
			{
				var tr = DoTrace();

				if ( !tr.Hit )
					return;

				var Name = Names[Random.Next( Names.Count )];
				var Bot = new Sandbox.Bot( Name );
				Bot.Client.Pawn.Position = tr.HitPosition;
				
				Bots.Add( Bot );
			} else if ( Input.Pressed( "reload" ) )
			{
				foreach ( var Bot in Bots )
				{
					Bot.Client.Kick();
				}
				
				Bots.Clear();
			}
		}
	}
}
