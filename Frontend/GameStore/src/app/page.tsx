import { Sidebar } from "@/Components/Layout/sidebar";
import { HeaderElements } from "@/Components/Layout/headerElements";
import GamesPage from "@/Components/Layout/gamesReturn";
import { MobileSidebarWrapper } from "@/Components/Layout/mobileSidebarWrapper";

export default function Home() {
  return (
    <div className="flex h-screen w-screen bg-[#121212] text-white overflow-hidden">

      {/* Sidebar — oculta em mobile, visível em lg+ */}
      <div className="hidden lg:block h-full shrink-0">
        <Sidebar />
      </div>

      {/* Conteúdo principal */}
      <div className="flex-1 flex flex-col overflow-y-auto bg-[#1a1a1a] min-w-0">
        <header className="px-4 py-3 sm:px-6 sm:py-4 border-b border-zinc-900">
          <div className="flex items-center gap-3">
            {/* Botão hamburguer — só aparece em mobile */}
            <MobileSidebarWrapper>
              <Sidebar />
            </MobileSidebarWrapper>

            <div className="flex-1">
              <HeaderElements />
            </div>
          </div>
        </header>

        <main className="p-4 sm:p-6">
          <GamesPage />
        </main>
      </div>
    </div>
  );
}