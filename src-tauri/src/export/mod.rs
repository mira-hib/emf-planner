use crate::project::ProjectData;
use std::io;

pub fn to_dxf(path: &str, data: &ProjectData) -> io::Result<()> {
    let mut drawing = dxf::Drawing::new();

    for source in &data.sources {
        let circle = dxf::entities::Circle {
            center: dxf::Point::new(source.lng, source.lat, 0.0),
            radius: 0.001, // ~100m in degrees
            ..Default::default()
        };
        drawing.add_entity(dxf::entities::Entity::new(
            dxf::entities::EntityType::Circle(circle),
        ));

        let text = dxf::entities::Text {
            location: dxf::Point::new(source.lng + 0.0005, source.lat + 0.0005, 0.0),
            value: source.name.clone(),
            text_height: 0.0003,
            ..Default::default()
        };
        drawing.add_entity(dxf::entities::Entity::new(
            dxf::entities::EntityType::Text(text),
        ));
    }

    drawing.save_file(path)
        .map_err(|e| io::Error::new(io::ErrorKind::Other, e))
}
