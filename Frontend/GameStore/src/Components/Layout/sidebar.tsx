import { 
  Factory,
  Cylinder,
  Layers, 
  Flame, 
  Store, 
  User, 
  Download, 
  Settings, 
  Handshake
} from "lucide-react";
import { Game } from "../Types/game";

async function getGames(page = 1, pageSize = 12): Promise<Game[]> {
  const res = await fetch(
    `http://localhost:5046/Games?PageNumber=${page}&PageSize=${pageSize}`,
    { next: { revalidate: 60 } }
  );

  if (!res.ok) throw new Error(`HTTP error: ${res.status}`);

  const json = await res.json();

  return Array.isArray(json) ? json : json.data ?? json.items ?? json.results ?? [];
}

export async function Sidebar() {
  const games = await getGames();

  return (
    <aside className="w-64 h-full bg-[#121212] text-zinc-400 flex flex-col justify-between p-4 border-r border-zinc-900 select-none">
      <div className="flex flex-col gap-6">

        <div className="flex items-center justify-between gap-2 px-2 py-1 text-white font-bold tracking-wider text-lg">
          <div>EPIC</div>
          <div className="cursor-pointer">...</div>
        </div>

        <nav className="flex flex-col gap-1">

          <button className="flex items-center gap-3 px-3 py-2 rounded-lg hover:text-zinc-200 hover:bg-zinc-900 text-sm transition-colors cursor-pointer">
            <Store className="w-4 h-4" />
            Store
          </button>

          <button className="flex items-center gap-3 px-3 py-2 rounded-lg text-zinc-200 bg-zinc-800/60 font-medium text-sm transition-colors cursor-pointer">
            <Layers className="w-4 h-4" />
            Library
          </button>

          <button className="flex items-center gap-3 px-3 py-2 rounded-lg hover:text-zinc-200 hover:bg-zinc-900 text-sm transition-colors cursor-pointer">
            <Factory className="w-4 h-4"/>
            Factory
          </button>

          <button className="flex items-center gap-3 px-3 py-2 rounded-lg hover:text-zinc-200 hover:bg-zinc-900 text-sm transition-colors cursor-pointer">
            <Flame className="w-4 h-4" />
            Unreal Engine
          </button>

          <button className="flex items-center gap-3 px-3 py-2 rounded-lg hover:text-zinc-200 hover:bg-zinc-900 text-sm transition-colors cursor-pointer">
            <Handshake className="w-4 h-4" />
            Community
          </button>

          <button className="flex items-center gap-3 px-3 py-2 rounded-lg hover:text-zinc-200 hover:bg-zinc-900 text-sm transition-colors cursor-pointer">
            <Cylinder className="w-4 h-4" />  
                Fab
          </button>
        </nav>

        <hr className="border-zinc-900" />

        <div className="flex flex-col gap-2">
          <span className="text-xs font-semibold tracking-wider text-zinc-600 px-3 uppercase">
            Quick Launch
          </span>
          <div className="flex flex-col gap-1">
            {games.
            filter((game) => game.isInstalled)
            .map((game) => (
              <button 
                key={game.name} 
                className="flex cursor-pointer items-center gap-3 px-3 py-2 text-left rounded-lg text-sm hover:text-zinc-200 hover:bg-zinc-900 group transition-all"
              >
                <div className="w-15 h-full bg-zinc-800 rounded-md overflow-hidden flex-shrink-0">
                    <img src={game.imageURL} alt={game.name} className="w-full h-full object-cover group-hover:scale-110 transition-transform" />
                </div>
               
                <div className="truncate flex-1 text-zinc-400 group-hover:text-zinc-200">
                  {game.name}
                </div>
              </button>
            ))}
          </div>
        </div>
      </div>

      <div className="flex flex-col gap-4">
        <div className="bg-zinc-900/50 p-3 rounded-xl border border-zinc-900 flex flex-col gap-2">
          <div className="flex items-center justify-between text-xs font-medium text-zinc-300">
            <div className="flex items-center gap-2">
              <Download className="w-3.5 h-3.5 text-blue-500 animate-pulse" />
              <span>Downloads</span>
            </div>
            <span className="text-zinc-500">1 out of 2</span>
          </div>
          <div className="w-full h-1 bg-zinc-800 rounded-full overflow-hidden">
            <div className="h-full bg-blue-500 rounded-full w-[45%]" />
          </div>
        </div>

        <hr className="border-zinc-900" />

        <div className="flex items-center justify-between px-2">
          <div className="flex items-center gap-3 cursor-pointer group">
            <div className="w-8 h-8 rounded-full bg-zinc-800 flex items-center justify-center border border-zinc-700 group-hover:border-zinc-500 transition-colors">
              <User className="w-4 h-4 text-zinc-300" />
            </div>
            <div className="flex flex-col max-w-[120px]">
              <span className="text-sm font-medium text-zinc-300 truncate group-hover:text-white transition-colors">
                Tun1n
              </span>
              <span className="text-[10px] text-green-500 font-semibold flex items-center gap-1">
                ● Online
              </span>
            </div>
          </div>
          
          <button className="p-2 text-zinc-500 hover:text-zinc-200 rounded-lg hover:bg-zinc-900 transition-colors">
            <Settings className="cursor-pointer w-4 h-4" />
          </button>
        </div>
      </div>

    </aside>
  );
}