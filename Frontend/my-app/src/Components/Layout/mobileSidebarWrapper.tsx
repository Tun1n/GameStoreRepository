"use client";

import { useState } from "react";
import { Menu, X } from "lucide-react";

export function MobileSidebarWrapper({
  children,
}: {
  children: React.ReactNode;
}) {
  const [open, setOpen] = useState(false);

  return (
    <>
      {/* Botão hamburguer — só visível abaixo de lg */}
      <button
        onClick={() => setOpen(true)}
        className="lg:hidden p-2 rounded-lg text-zinc-400 hover:text-white hover:bg-zinc-800 transition-colors shrink-0"
        aria-label="Abrir menu"
      >
        <Menu className="w-5 h-5" />
      </button>

      {/* Overlay escuro */}
      {open && (
        <div
          className="fixed inset-0 z-40 bg-black/60 lg:hidden"
          onClick={() => setOpen(false)}
        />
      )}

      {/* Drawer da sidebar */}
      <div
        className={`
          fixed top-0 left-0 z-50 h-full
          transform transition-transform duration-300 ease-in-out
          lg:hidden
          ${open ? "translate-x-0" : "-translate-x-full"}
        `}
      >
        {/* Botão fechar */}
        <button
          onClick={() => setOpen(false)}
          className="absolute top-4 right-[-44px] z-50 p-2 rounded-lg bg-zinc-800 text-zinc-400 hover:text-white"
          aria-label="Fechar menu"
        >
          <X className="w-5 h-5" />
        </button>


        {children}
      </div>
    </>
  );
}
