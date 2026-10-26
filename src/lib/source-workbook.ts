import ExcelJS from "exceljs";
import type { PlannerProject, RadiatingSource } from "../types";

/** ExcelReports.ExcelExportPRTO1, recovered 7.0: A:S, one header row. */
export const NATIVE_SOURCE_COLUMNS: [keyof RadiatingSource | "frequency_span" | "existing", string][] = [
  ["name", "Radiation source name"], ["sector_id", "Sector ID"],
  ["service_type", "Type of service"], ["frequency_span", "Frequency MHz"],
  ["power_w", "Transmitter power W"], ["num_tx", "Number TX"],
  ["total_attenuation_db", "Total atten dB"], ["antenna_model", "Antenna model"],
  ["azimuth_deg", "Antenna azimuth deg"], ["gain_dbi", "Antenna gain dBi"],
  ["height_m", "Antenna height m"], ["antenna_size_m", "Antenna size m"],
  ["mechanical_downtilt_deg", "Mechanical downtilt deg"],
  ["electrical_downtilt_deg", "Electrical downtilt deg"],
  ["beam_width_h_deg", "Horizontal beamwidth deg"], ["beam_width_v_deg", "Vertical beamwidth deg"],
  ["location", "Location"], ["existing", "Existing / New"], ["owner", "Owner"],
];

export async function sourceWorkbookBytes(project: PlannerProject): Promise<Uint8Array> {
  const book = new ExcelJS.Workbook();
  book.creator = "EMF Planner";
  const sheet = book.addWorksheet("Sources");
  sheet.addRow(NATIVE_SOURCE_COLUMNS.map(([, title]) => title));
  for (const source of project.sources.filter((s) => s.active !== false)) {
    sheet.addRow(NATIVE_SOURCE_COLUMNS.map(([key]) => {
      if (key === "frequency_span") return source.frequency_end_mhz && source.frequency_end_mhz !== source.frequency_mhz
        ? `${source.frequency_mhz}-${source.frequency_end_mhz}` : source.frequency_mhz;
      if (key === "existing") return source.is_existing === false ? "New" : "Existing";
      return source[key] as string | number;
    }));
  }
  sheet.views = [{ state: "frozen", ySplit: 1 }];
  sheet.getRow(1).font = { bold: true };
  sheet.columns.forEach((column) => { column.width = 23; });
  return new Uint8Array(await book.xlsx.writeBuffer());
}
