import { Bell, Users, CircleQuestionMark } from 'lucide-react';

export function HeaderElements() {
  return (
    <div className="flex items-center justify-between gap-3 w-full">

      {/* Título — some em mobile pra economizar espaço */}
      <h1 className="hidden sm:block text-2xl lg:text-4xl font-bold shrink-0">
        Library
      </h1>

      {/* Search — cresce pra preencher o espaço disponível */}
      <div className="flex flex-1 justify-center items-center cursor-pointer bg-cross-search rounded-full px-4 h-9 sm:h-10 max-w-xs sm:max-w-sm lg:max-w-md mx-auto">
        <span className="text-sm sm:text-base truncate">Search</span>
      </div>

      {/* Ícones — sempre visíveis */}
      <div className="flex items-center gap-3 sm:gap-5 lg:gap-7 shrink-0">
        <button className="cursor-pointer text-zinc-400 hover:text-white transition-colors" aria-label="Notificações">
          <Bell className="w-5 h-5" />
        </button>
        <button className="cursor-pointer text-zinc-400 hover:text-white transition-colors" aria-label="Amigos">
          <Users className="w-5 h-5" />
        </button>
        <button className="cursor-pointer text-zinc-400 hover:text-white transition-colors" aria-label="Ajuda">
          <CircleQuestionMark className="w-5 h-5" />
        </button>
      </div>

    </div>
  );
}