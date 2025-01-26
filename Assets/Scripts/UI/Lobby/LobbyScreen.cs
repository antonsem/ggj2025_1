using ExtraTools.UI.Screen;
using TheBubbleHell.UI.Lobby.Panels.Lobby;

namespace TheBubbleHell.UI.Lobby
{
	public class LobbyScreen : ScreenBase
	{
		public void ShowLobby()
		{
			
			ShowPanelAsync<LobbyPanel>();
		}
	}
}
