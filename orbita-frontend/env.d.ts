/// <reference types="vite/client" />

interface ImportMetaEnv {
  readonly VITE_ORBITA_API: string
}

interface ImportMeta {
  readonly env: ImportMetaEnv
}