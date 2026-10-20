using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class Localization
{
	private static string[,] LC = new string[201, 4]
	{
		{
			Conversions.ToString(0),
			"Удалить",
			"Remove",
			""
		},
		{
			Conversions.ToString(1),
			"выбранных строк (источников излучения)",
			"selected rows (radiating sources)",
			""
		},
		{
			Conversions.ToString(2),
			"Файлы описания антенны MSI",
			"Antenna pattern files MSI",
			""
		},
		{
			Conversions.ToString(3),
			"Выбранный файл имеет неправильный формат",
			"The selected file is not in the correct format",
			""
		},
		{
			Conversions.ToString(4),
			"Указать вручную",
			"Specify manually",
			""
		},
		{
			Conversions.ToString(5),
			"Файлы описания антенны MSI",
			"Antenna pattern files MSI",
			""
		},
		{
			Conversions.ToString(6),
			"Диаграмма направленности антенны не задана",
			"Antenna pattern not set",
			""
		},
		{
			Conversions.ToString(7),
			"Не выбран файл ДН антенны",
			"Antenna pattern file not selected",
			""
		},
		{
			Conversions.ToString(8),
			"Буфер обмена пуст",
			"Clipboard is empty",
			""
		},
		{
			Conversions.ToString(9),
			"Не указан путь к папке с файлами ДН антенн. Укажите путь к папке в настройках приложения",
			"The path to the folder with antenna pattern files is not specified. Specify the path to the folder in the application settings",
			""
		},
		{
			Conversions.ToString(10),
			"Удалить растровое изображение из проекта?",
			"Remove the bitmap from the project?",
			""
		},
		{
			Conversions.ToString(11),
			"Перенести точку привязки и отрезок масштабирования на растровом изображении в центр экрана?",
			"Set the reference point and the reference line on the image to the screen center?",
			""
		},
		{
			Conversions.ToString(12),
			"Файлы растровых изображений",
			"Bitmap files",
			""
		},
		{
			Conversions.ToString(13),
			"Перенести точку привязки и отрезок масштабирования на карте в центр экрана?",
			"Set the reference point and the reference line on the base map to the screen center?",
			""
		},
		{
			Conversions.ToString(14),
			"Tочка привязки",
			"Reference point",
			""
		},
		{
			Conversions.ToString(15),
			"Опорный отрезок",
			"Reference line",
			""
		},
		{
			Conversions.ToString(16),
			"Отсутствуют активные источники излучения",
			"No active radiating sources",
			""
		},
		{
			Conversions.ToString(17),
			"Широта",
			"Latitude",
			""
		},
		{
			Conversions.ToString(18),
			"Долгота",
			"Longitude",
			""
		},
		{
			Conversions.ToString(19),
			"Отметка земли",
			"Elevation",
			""
		},
		{
			Conversions.ToString(20),
			"Неверное значение",
			"Incorrect value",
			""
		},
		{
			Conversions.ToString(21),
			"Перенести точку начала координат в центр экрана?",
			"Set the origin point to the screen center?",
			""
		},
		{
			Conversions.ToString(22),
			"Отсутствующие здания в пределах экрана будут загружены из БД OSM. Продолжить?",
			"Missing buildings within the screen will be loaded from the OSM database. Proceed?",
			""
		},
		{
			Conversions.ToString(23),
			"Добавлено",
			"Added",
			""
		},
		{
			Conversions.ToString(24),
			"зданий",
			"buildings",
			""
		},
		{
			Conversions.ToString(25),
			"Удалить все здания?",
			"Delete all buildings?",
			""
		},
		{
			Conversions.ToString(26),
			"Здание не выделено",
			"Building not selected",
			""
		},
		{
			Conversions.ToString(27),
			"Удалить выделенное здание?",
			"Delete the selected building?",
			""
		},
		{
			Conversions.ToString(28),
			"Заменить отметки нулей всех зданий на значения на основании отметок рельефа в точках углов этих зданий?",
			"Fill in all building base elevations based on the terrain data?",
			""
		},
		{
			Conversions.ToString(29),
			"эт",
			"fl",
			""
		},
		{
			Conversions.ToString(30),
			"Введенное значение",
			"Entered value",
			""
		},
		{
			Conversions.ToString(31),
			"является недопустимым для данного параметра",
			"is invalid for this parameter",
			""
		},
		{
			Conversions.ToString(32),
			"Нет выделенных горизонталей",
			"No selected elevation contours",
			""
		},
		{
			Conversions.ToString(33),
			"Нет выделенных объектов",
			"No selected objects",
			""
		},
		{
			Conversions.ToString(34),
			"Использовать данные DEM в качестве точек рельефа? Ранее введенные данные рельефа будут удалены",
			"Use DEM data as elevation points? Previously entered terrain data will be deleted",
			""
		},
		{
			Conversions.ToString(35),
			"Удалить все объекты?",
			"Delete all objects?",
			""
		},
		{
			Conversions.ToString(36),
			"Удалить выделенный объект?",
			"Delete selected object?",
			""
		},
		{
			Conversions.ToString(37),
			"Заменить все линии и надписи?",
			"Replace all lines and labels?",
			""
		},
		{
			Conversions.ToString(38),
			"Начало координат",
			"Origin",
			""
		},
		{
			Conversions.ToString(39),
			"Нет выбранных источников излучения",
			"No radiating sources (antennas) selected",
			""
		},
		{
			Conversions.ToString(40),
			"Перенести выбранные антенны в точку активной антенны?",
			"Move all selected antennas to the current antenna point?",
			""
		},
		{
			Conversions.ToString(41),
			"Перенести выбранные антенны в точку начала координат?",
			"Move all selected antennas to the origin?",
			""
		},
		{
			Conversions.ToString(42),
			"Перенести начало координат в точку текущей антенны?",
			"Move origin to current antenna point?",
			""
		},
		{
			Conversions.ToString(43),
			"Перенести выбранные источники излучения в центр экрана?",
			"Move selected antennas to the screen center?",
			""
		},
		{
			Conversions.ToString(44),
			"Группа контрольных точек 1",
			"Control point group 1",
			""
		},
		{
			Conversions.ToString(45),
			"Нет выбранной контрольной точки",
			"No control points selected",
			""
		},
		{
			Conversions.ToString(46),
			"Отсутствуют источники излучения",
			"No radiating sources",
			""
		},
		{
			Conversions.ToString(47),
			"Отсутствуют контрольные точки",
			"No control points",
			""
		},
		{
			Conversions.ToString(48),
			"выбранных строк (контрольных точек)",
			"selected rows (control points)",
			""
		},
		{
			Conversions.ToString(49),
			"выбранных строк (групп контрольных точек)",
			"selected rows (control point groups)",
			""
		},
		{
			Conversions.ToString(50),
			"выбранных строк (высот контрольных точек)",
			"selected rows (control point heights)",
			""
		},
		{
			Conversions.ToString(51),
			"Невозможно удалить все высоты для контрольной точки",
			"Unable to remove all heights for the control point",
			""
		},
		{
			Conversions.ToString(52),
			"Не выбрана группа контрольных точек",
			"No control point groups selected",
			""
		},
		{
			Conversions.ToString(53),
			"Перенести выбранные КТ в центр экрана?",
			"Move selected control points to the screen center?",
			""
		},
		{
			Conversions.ToString(54),
			"Земля",
			"Groung",
			""
		},
		{
			Conversions.ToString(55),
			"Здание",
			"Building",
			""
		},
		{
			Conversions.ToString(56),
			"Окно",
			"Window",
			""
		},
		{
			Conversions.ToString(57),
			"+2 м на верхнем этаже",
			"+2 m on the top floor",
			""
		},
		{
			Conversions.ToString(58),
			"+2 м над кровлей",
			"+2 m above the roof",
			""
		},
		{
			Conversions.ToString(59),
			"Перенести область расчета в центр экрана?",
			"Move the calculation grid to the center of the screen?",
			""
		},
		{
			Conversions.ToString(60),
			"Обновить таблицу горизонтальных сечений?",
			"Fill in the list of horizontal cuts with the heights of antennas of active radiating sources",
			""
		},
		{
			Conversions.ToString(61),
			"Добавить в список горизонтальных сечений высоты максимумов вертикальных сечений (если выполнен расчет вертикальных сечений)?",
			"Add to the list of horizontal cuts the heights of the maxima of vertical cuts (if a calculation has already been performed in vertical cuts)",
			""
		},
		{
			Conversions.ToString(62),
			"Линейка не активирована",
			"Ruler is not activated",
			""
		},
		{
			Conversions.ToString(63),
			"Добавлено вертикальное сечение по азимуту",
			"Added vertical cut along azimuth",
			""
		},
		{
			Conversions.ToString(64),
			"Выполнить расчет и отобразить это сечение?",
			"Calculate and display this cut?",
			""
		},
		{
			Conversions.ToString(65),
			"Отсутствуют горизонтальные сечения",
			"No horizontal cuts",
			""
		},
		{
			Conversions.ToString(66),
			"Отсутствуют вертикальные сечения",
			"No vertical cuts",
			""
		},
		{
			Conversions.ToString(67),
			"выбранных строк (вертикальных сечений)",
			"selected rows (vertical cuts)",
			""
		},
		{
			Conversions.ToString(68),
			"Отсутствуют группы контрольных точек",
			"No control point groups",
			""
		},
		{
			Conversions.ToString(69),
			"Обновить таблицу вертикальных сечений?",
			"Fill in the list of vertical cuts?",
			""
		},
		{
			Conversions.ToString(70),
			"Удалить все точки измерения для всех сечений?",
			"Delete all measuring points for all cuts?",
			""
		},
		{
			Conversions.ToString(71),
			"Расчет не может быть продолжен",
			"Calculation cannot be continued",
			""
		},
		{
			Conversions.ToString(72),
			"Расположение КТ",
			"Location of control point",
			""
		},
		{
			Conversions.ToString(73),
			"указано как 'Building', но на ситуационном плане отсутствуют здания. Следует добавить здание на ситуационный план",
			"is listed as 'Building' but there are no buildings on the map. A building should be added to the map",
			""
		},
		{
			Conversions.ToString(74),
			"указано как 'Building', но данная КТ находится за пределами контуров зданий. Следует перенести КТ в пределы контура здания",
			" is listed as 'Building', but this control point is outside a building contour. The control point should be moved within the contour of a building",
			""
		},
		{
			Conversions.ToString(75),
			"указано как 'Ground', но точка находится в пределах контура здания. Следует переместить точку за пределы здания",
			"is listed as 'Ground', but the control point is within the contour of a building. Move the control point outside the building",
			""
		},
		{
			Conversions.ToString(76),
			"Некорректно указаны параметры ПДУ",
			"EMF limits are incorrect. Check EMF limits",
			""
		},
		{
			Conversions.ToString(77),
			"Расположение источника излучения",
			"Location of radiating source",
			""
		},
		{
			Conversions.ToString(78),
			"указано 'Tower', но точка установки антенны находится внутри контура здания. Следует изменить место установки антенны",
			"is listed as 'Tower', but the radiating source point is within the contour of a building. Move the radiating source point outside the building",
			""
		},
		{
			Conversions.ToString(79),
			"указано 'Roof', но на ситуационном плане отсутствуют здания. Следует добавить здание на ситуационный план",
			"is listed as 'Roof' but there are no buildings on the map. A building should be added to the map",
			""
		},
		{
			Conversions.ToString(80),
			"указано 'Roof', но высота антенны меньше высоты здания на котором она установлена. Следует изменить высоту антенны либо здания",
			"is listed as 'Roof', but the height of the antenna is less than the height of the building on which it is installed. Change the height of the antenna or the building",
			""
		},
		{
			Conversions.ToString(81),
			"указано 'Roof', но точка, в которой установлена антенна, находится за пределами контуров зданий. Следует перенести антенну в пределы контура здания.",
			"is listed as 'Roof', but the antenna point is outside the building contours. The antenna point should be moved within the contour of a building",
			""
		},
		{
			Conversions.ToString(82),
			"указано 'Facade', но на ситуационном плане отсутствуют здания. Следует добавить здание на ситуационный план",
			"is listed as 'Facade' but there are no buildings on the map. A building should be added to the map",
			""
		},
		{
			Conversions.ToString(83),
			"указано 'Facade', но точка, в которой установлена антенна, находится далее 2 метров от ближайшего здания. Следует перенести антенну ближе к контуру здания",
			"is listed as 'Facade', but the antenna point is more than 2 meters from the nearest building. Move the antenna closer to the contour of the building",
			""
		},
		{
			Conversions.ToString(84),
			"Не задана диаграмма направленности антенны для источника излучения",
			"Antenna pattern not set for radiating source",
			""
		},
		{
			Conversions.ToString(85),
			"Отсутствуют активные сечения",
			"No active cuts",
			""
		},
		{
			Conversions.ToString(86),
			"Сечения не отсортированы в порядке возрастания их высоты. Воспользуйтесь соответствующей иконкой в панели инструментов для сортировки сечений",
			"Cuts (lines) are not sorted in ascending order of their height. Use the icon in the toolbar to sort cuts",
			""
		},
		{
			Conversions.ToString(87),
			"Расчет в контрольных точках не может быть продолжен",
			"Calculation in control points cannot be continued",
			""
		},
		{
			Conversions.ToString(88),
			"Отсутствуют активные КТ",
			"No active control points",
			""
		},
		{
			Conversions.ToString(89),
			"указано как 'Window', но на ситуационном плане отсутствуют здания. Следует добавить здание на ситуационный план",
			"is listed as 'Window' but there are no buildings on the map. A building should be added to the map",
			""
		},
		{
			Conversions.ToString(90),
			"указано как 'Window', но данная КТ находится за пределами контуров зданий. Следует перенести КТ в пределы контура здания",
			" is listed as 'Window', but this control point is outside a building contour. The control point should be moved within the contour of a building",
			""
		},
		{
			Conversions.ToString(91),
			"Изменить параметры ПДУ?",
			"Change EMF limit parameters?",
			""
		},
		{
			Conversions.ToString(92),
			"сечение на отметке",
			"cut at height",
			""
		},
		{
			Conversions.ToString(93),
			"Вертикальное сечение по азимуту",
			"Vertical cut along azimuth",
			""
		},
		{
			Conversions.ToString(94),
			"вдоль направления антенны",
			"along direction of antenna",
			""
		},
		{
			Conversions.ToString(95),
			"Настройки базовых карт заданы неверно",
			"Basemap settings are incorrect",
			""
		},
		{
			Conversions.ToString(96),
			"Удалить все введенные данные?",
			"Close the current project and create a new one?",
			""
		},
		{
			Conversions.ToString(97),
			"Файл проекта не найден",
			"Project file not found",
			""
		},
		{
			Conversions.ToString(98),
			"Удалить все объекты за пределами области экрана?",
			"Delete all objects outside the screen area?",
			""
		},
		{
			Conversions.ToString(99),
			"Удалить все объекты за пределами области расчета?",
			"Delete all objects outside the calculation area?",
			""
		},
		{
			Conversions.ToString(100),
			"Нет выбранной антенны",
			"No antenna selected",
			""
		},
		{
			Conversions.ToString(101),
			"Сохранить файл проекта?",
			"Save project file?",
			""
		},
		{
			Conversions.ToString(102),
			"Отсутствует файл с характеристиками кабелей",
			"Missing file with cable parameters",
			""
		},
		{
			Conversions.ToString(103),
			"Не верно задан угол поворота",
			"Rotation angle incorrect",
			""
		},
		{
			Conversions.ToString(104),
			"Error",
			"Error",
			""
		},
		{
			Conversions.ToString(105),
			"Введенные данные неверны",
			"The entered data is not correct",
			""
		},
		{
			Conversions.ToString(106),
			"У вас установлена актуальная версия программы EMF Planner 7.1",
			"No new version available. Current version is EMF Planner 7.1",
			""
		},
		{
			Conversions.ToString(107),
			"Невозможно получить информацию о наличии новой версии",
			"Unable to get new version information",
			""
		},
		{
			Conversions.ToString(108),
			"Не указан путь к файлу шаблона отчета, либо указанный файл не существует",
			"The path to the report template file is not specified, or the specified file does not exist",
			""
		},
		{
			Conversions.ToString(109),
			"Частота измерений задана некорректно",
			"Wrong frequency for EMF limit of E-field strength measurement",
			""
		},
		{
			Conversions.ToString(110),
			"Нет выбранных файлов ДН для построения огибающей",
			"No selected antenna pattern files to create the envelope",
			""
		},
		{
			Conversions.ToString(111),
			"В строке 2 должны быть указаны наименование и координаты первой контрольной точки!",
			"Line 2 must contain the name and the coordinates of the first control point.",
			""
		},
		{
			Conversions.ToString(112),
			"",
			"",
			""
		},
		{
			Conversions.ToString(113),
			"",
			"",
			""
		},
		{
			Conversions.ToString(114),
			"",
			"",
			""
		},
		{
			Conversions.ToString(115),
			"",
			"",
			""
		},
		{
			Conversions.ToString(116),
			"",
			"",
			""
		},
		{
			Conversions.ToString(117),
			"",
			"",
			""
		},
		{
			Conversions.ToString(118),
			"",
			"",
			""
		},
		{
			Conversions.ToString(119),
			"",
			"",
			""
		},
		{
			Conversions.ToString(120),
			"",
			"",
			""
		},
		{
			Conversions.ToString(121),
			"",
			"",
			""
		},
		{
			Conversions.ToString(122),
			"",
			"",
			""
		},
		{
			Conversions.ToString(123),
			"",
			"",
			""
		},
		{
			Conversions.ToString(124),
			"",
			"",
			""
		},
		{
			Conversions.ToString(125),
			"",
			"",
			""
		},
		{
			Conversions.ToString(126),
			"",
			"",
			""
		},
		{
			Conversions.ToString(127),
			"",
			"",
			""
		},
		{
			Conversions.ToString(128),
			"",
			"",
			""
		},
		{
			Conversions.ToString(129),
			"",
			"",
			""
		},
		{
			Conversions.ToString(130),
			"",
			"",
			""
		},
		{
			Conversions.ToString(131),
			"",
			"",
			""
		},
		{
			Conversions.ToString(132),
			"",
			"",
			""
		},
		{
			Conversions.ToString(133),
			"",
			"",
			""
		},
		{
			Conversions.ToString(134),
			"",
			"",
			""
		},
		{
			Conversions.ToString(135),
			"",
			"",
			""
		},
		{
			Conversions.ToString(136),
			"",
			"",
			""
		},
		{
			Conversions.ToString(137),
			"",
			"",
			""
		},
		{
			Conversions.ToString(138),
			"",
			"",
			""
		},
		{
			Conversions.ToString(139),
			"",
			"",
			""
		},
		{
			Conversions.ToString(140),
			"",
			"",
			""
		},
		{
			Conversions.ToString(141),
			"",
			"",
			""
		},
		{
			Conversions.ToString(142),
			"",
			"",
			""
		},
		{
			Conversions.ToString(143),
			"",
			"",
			""
		},
		{
			Conversions.ToString(144),
			"",
			"",
			""
		},
		{
			Conversions.ToString(145),
			"",
			"",
			""
		},
		{
			Conversions.ToString(146),
			"",
			"",
			""
		},
		{
			Conversions.ToString(147),
			"",
			"",
			""
		},
		{
			Conversions.ToString(148),
			"",
			"",
			""
		},
		{
			Conversions.ToString(149),
			"",
			"",
			""
		},
		{
			Conversions.ToString(150),
			"",
			"",
			""
		},
		{
			Conversions.ToString(151),
			"",
			"",
			""
		},
		{
			Conversions.ToString(152),
			"",
			"",
			""
		},
		{
			Conversions.ToString(153),
			"",
			"",
			""
		},
		{
			Conversions.ToString(154),
			"",
			"",
			""
		},
		{
			Conversions.ToString(155),
			"",
			"",
			""
		},
		{
			Conversions.ToString(156),
			"",
			"",
			""
		},
		{
			Conversions.ToString(157),
			"",
			"",
			""
		},
		{
			Conversions.ToString(158),
			"",
			"",
			""
		},
		{
			Conversions.ToString(159),
			"",
			"",
			""
		},
		{
			Conversions.ToString(160),
			"",
			"",
			""
		},
		{
			Conversions.ToString(161),
			"",
			"",
			""
		},
		{
			Conversions.ToString(162),
			"",
			"",
			""
		},
		{
			Conversions.ToString(163),
			"",
			"",
			""
		},
		{
			Conversions.ToString(164),
			"",
			"",
			""
		},
		{
			Conversions.ToString(165),
			"",
			"",
			""
		},
		{
			Conversions.ToString(166),
			"",
			"",
			""
		},
		{
			Conversions.ToString(167),
			"",
			"",
			""
		},
		{
			Conversions.ToString(168),
			"",
			"",
			""
		},
		{
			Conversions.ToString(169),
			"",
			"",
			""
		},
		{
			Conversions.ToString(170),
			"",
			"",
			""
		},
		{
			Conversions.ToString(171),
			"",
			"",
			""
		},
		{
			Conversions.ToString(172),
			"",
			"",
			""
		},
		{
			Conversions.ToString(173),
			"",
			"",
			""
		},
		{
			Conversions.ToString(174),
			"",
			"",
			""
		},
		{
			Conversions.ToString(175),
			"",
			"",
			""
		},
		{
			Conversions.ToString(176),
			"",
			"",
			""
		},
		{
			Conversions.ToString(177),
			"",
			"",
			""
		},
		{
			Conversions.ToString(178),
			"",
			"",
			""
		},
		{
			Conversions.ToString(179),
			"",
			"",
			""
		},
		{
			Conversions.ToString(180),
			"",
			"",
			""
		},
		{
			Conversions.ToString(181),
			"",
			"",
			""
		},
		{
			Conversions.ToString(182),
			"",
			"",
			""
		},
		{
			Conversions.ToString(183),
			"",
			"",
			""
		},
		{
			Conversions.ToString(184),
			"",
			"",
			""
		},
		{
			Conversions.ToString(185),
			"",
			"",
			""
		},
		{
			Conversions.ToString(186),
			"",
			"",
			""
		},
		{
			Conversions.ToString(187),
			"",
			"",
			""
		},
		{
			Conversions.ToString(188),
			"",
			"",
			""
		},
		{
			Conversions.ToString(189),
			"",
			"",
			""
		},
		{
			Conversions.ToString(190),
			"",
			"",
			""
		},
		{
			Conversions.ToString(191),
			"",
			"",
			""
		},
		{
			Conversions.ToString(192),
			"",
			"",
			""
		},
		{
			Conversions.ToString(193),
			"",
			"",
			""
		},
		{
			Conversions.ToString(194),
			"",
			"",
			""
		},
		{
			Conversions.ToString(195),
			"",
			"",
			""
		},
		{
			Conversions.ToString(196),
			"",
			"",
			""
		},
		{
			Conversions.ToString(197),
			"",
			"",
			""
		},
		{
			Conversions.ToString(198),
			"",
			"",
			""
		},
		{
			Conversions.ToString(199),
			"",
			"",
			""
		},
		{
			Conversions.ToString(200),
			"",
			"",
			""
		}
	};

	public static string LocMSG(short IND)
	{
		return LC[IND, 2];
	}
}
