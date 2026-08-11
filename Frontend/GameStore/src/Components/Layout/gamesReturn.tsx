"use client";

import { Game } from "@/Components/Types/game";
import { CirclePlus, ChevronLeft, ChevronRight } from "lucide-react";
import { useEffect, useState } from "react";
import { useRouter, useSearchParams, usePathname } from "next/navigation";

const PAGE_SIZE = 12;

async function getGames(page = 1, pageSize = PAGE_SIZE): Promise<Game[]> {
  try {
    const res = await fetch(
      `http://localhost:5046/Games?PageNumber=${page}&PageSize=${pageSize}`,
      { next: { revalidate: 60 } },
    );

    if (!res.ok) return [];

    const json = await res.json();

    return Array.isArray(json)
      ? json
      : (json.data ?? json.items ?? json.results ?? []);
  } catch {
    return [];
  }
}

export default function GamesPage() {
  const router = useRouter();
  const pathname = usePathname();
  const searchParams = useSearchParams();

  const currentPage = Number(searchParams.get("page") ?? "1");
  const filterParam =
    (searchParams.get("filter") as "all" | "installed") ?? "all";

  const [games, setGames] = useState<Game[]>([]);
  const [hasNextPage, setHasNextPage] = useState(false);

  useEffect(() => {
    getGames(currentPage, PAGE_SIZE).then((data) => {
      setGames(data);
      setHasNextPage(data.length === PAGE_SIZE);
    });
  }, [currentPage]);

  const filteredGames =
    filterParam === "installed" ? games.filter((g) => g.isInstalled) : games;

  function navigate(params: Record<string, string>) {
    const next = new URLSearchParams(searchParams.toString());
    Object.entries(params).forEach(([k, v]) => next.set(k, v));
    router.push(`${pathname}?${next.toString()}`);
  }

  function setFilter(value: "all" | "installed") {
    navigate({ filter: value, page: "1" });
  }

  function goToPage(page: number) {
    navigate({ page: String(page) });
  }

  return (
    <main className="p-4 sm:p-6 lg:p-8 max-w-screen-xl mx-auto">
      {/* Nav principal */}
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

      {/* Filtro + Paginação na mesma linha */}
      <div className="flex items-center justify-between mb-6">
        {/* Filtro Installed / All */}
        <div className="inline-flex items-center h-10 gap-4 rounded-2xl bg-hover-images px-4">
          <button
            onClick={() => setFilter("installed")}
            className={`cursor-pointer text-sm sm:text-base transition-all ${
              filterParam === "installed" ? "font-bold underline" : ""
            }`}
          >
            Installed
          </button>
          <div className="flex items-center h-full border-l border-black pl-4">
            <button
              onClick={() => setFilter("all")}
              className={`cursor-pointer text-sm sm:text-base transition-all ${
                filterParam === "all" ? "font-bold underline" : ""
              }`}
            >
              All
            </button>
          </div>
        </div>

        {/* Paginação */}
        <div className="inline-flex items-center h-10 gap-3 rounded-2xl bg-hover-images px-4">
          <button
            onClick={() => goToPage(currentPage - 1)}
            disabled={currentPage <= 1}
            className="cursor-pointer disabled:opacity-30 disabled:cursor-not-allowed hover:opacity-70 transition-opacity"
            aria-label="Página anterior"
          >
            <ChevronLeft className="w-4 h-4" />
          </button>

          <span className="text-sm sm:text-base font-semibold min-w-[3rem] text-center">
            Page {currentPage}
          </span>

          <button
            onClick={() => goToPage(currentPage + 1)}
            disabled={!hasNextPage}
            className="cursor-pointer disabled:opacity-30 disabled:cursor-not-allowed hover:opacity-70 transition-opacity"
            aria-label="Próxima página"
          >
            <ChevronRight className="w-4 h-4" />
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
