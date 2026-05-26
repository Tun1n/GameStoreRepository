"use client";

import { Game } from "@/Components/Types/game";
import { CirclePlus } from "lucide-react";
import { useEffect, useState } from "react";

async function getGames(page = 1, pageSize = 12): Promise<Game[]> {
  const res = await fetch(
    `http://localhost:5046/Games?PageNumber=${page}&PageSize=${pageSize}`,
    { next: { revalidate: 60 } },
  );

  if (!res.ok) throw new Error(`HTTP error: ${res.status}`);

  const json = await res.json();

  return Array.isArray(json)
    ? json
    : (json.data ?? json.items ?? json.results ?? []);
}

export default function GamesPage() {
  const [games, setGames] = useState<Game[]>([]);
  const [filter, setFilter] = useState<"all" | "installed">("all");

  useEffect(() => {
    getGames().then(setGames);
  }, []);

  const filteredGames =
    filter === "installed" ? games.filter((g) => g.isInstalled) : games;

  return (
    <main className="p-4 sm:p-6 lg:p-8 max-w-screen-xl mx-auto">
      {/* Nav principal — scroll horizontal em mobile */}
      <nav className="flex items-center gap-6 sm:gap-10 lg:gap-16 text-lg sm:text-xl lg:text-2xl font-bold mb-6 sm:mb-8 overflow-x-auto pb-2 scrollbar-none whitespace-nowrap">
        <span className="cursor-pointer hover:underline shrink-0">
          All games
        </span>
        <span className="text-zinc-600 cursor-pointer hover:underline shrink-0">
          Favorites
        </span>
        <span className="text-zinc-600 cursor-pointer hover:underline shrink-0">
          Play Next
        </span>
        <span className="text-zinc-600 cursor-pointer hover:underline shrink-0">
          Go to&apos;s
        </span>
        <span className="flex items-center gap-2 text-zinc-600 cursor-pointer hover:underline shrink-0">
          Multiplayer <CirclePlus className="w-5 h-5" />
        </span>
      </nav>

      {/* Filtro Installed / All */}
      <div className="inline-flex items-center h-10 gap-4 mb-6 rounded-2xl bg-hover-images px-4">
        <button
          onClick={() => setFilter("installed")}
          className={`cursor-pointer text-sm sm:text-base transition-all ${
            filter === "installed" ? "font-bold underline" : ""
          }`}
        >
          Installed
        </button>
        <div className="flex items-center h-full border-l border-black pl-4">
          <button
            onClick={() => setFilter("all")}
            className={`cursor-pointer text-sm sm:text-base transition-all ${
              filter === "all" ? "font-bold underline" : ""
            }`}
          >
            All
          </button>
        </div>
      </div>

      {/* Grid de jogos */}
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4 sm:gap-5 lg:gap-7">
  {filteredGames.map((game) => (
    <div
      key={game.name}
      className="group cursor-pointer rounded-xl overflow-hidden shadow-md hover:shadow-xl transition-shadow duration-300"
    >
      {/* Container controla a proporção — imagem preenche tudo */}
      <div className="relative w-full aspect-[3/2]">
        <img
          src={game.imageURL}
          alt={game.name}
          className="absolute inset-0 w-full h-full object-cover group-hover:scale-105 transition-transform duration-300"
        />
      </div>

      <div className="flex items-center justify-between px-3 py-2">
        <h2 className="font-semibold text-sm truncate flex-1 mr-2">
          {game.name}
        </h2>
        <button
          className="text-xl leading-none px-1 hover:bg-zinc-800 rounded transition-colors"
          aria-label="Opções"
        >
          ···
        </button>
      </div>
    </div>
  ))}
</div>

      {/* Estado vazio */}
      {filteredGames.length === 0 && (
        <p className="text-center text-zinc-500 mt-16 text-sm sm:text-base">
          Nenhum jogo encontrado.
        </p>
      )}
    </main>
  );
}
