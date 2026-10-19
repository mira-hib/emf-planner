import { Save, FolderOpen, FileDown, Settings } from "lucide-react";

interface Props {
  projectName: string;
  onSave: () => void;
  onOpen: () => void;
  onExport: () => void;
  onSettings: () => void;
}

export default function Toolbar({ projectName, onSave, onOpen, onExport, onSettings }: Props) {
  return (
    <div className="toolbar">
      <div className="toolbar-brand">
        <span className="brand-icon">📡</span>
        <span className="brand-name">EMF Planner</span>
        <span className="brand-version">v0.1</span>
      </div>
      <span className="toolbar-project">{projectName}</span>
      <div className="toolbar-actions">
        <button className="btn-toolbar" onClick={onOpen} title="Ouvrir">
          <FolderOpen size={18} />
        </button>
        <button className="btn-toolbar" onClick={onSave} title="Sauvegarder">
          <Save size={18} />
        </button>
        <button className="btn-toolbar" onClick={onExport} title="Exporter DXF">
          <FileDown size={18} />
        </button>
        <button className="btn-toolbar" onClick={onSettings} title="Paramètres">
          <Settings size={18} />
        </button>
      </div>
    </div>
  );
}
