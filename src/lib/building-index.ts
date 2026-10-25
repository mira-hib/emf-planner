import type { Building } from "../types";
interface IndexedBuilding { building: Building; minX: number; maxX: number; minY: number; maxY: number; order: number }
export class BuildingIndex {
  readonly entries: IndexedBuilding[];
  private bins = new Map<string, IndexedBuilding[]>();
  private large: IndexedBuilding[] = [];
  private cell = 10;
  private bounds = [0, 0, 0, 0];
  constructor(buildings: Building[]) {
    this.entries = buildings.filter(b => b.active).map((building, order) => {
      let minX=Infinity,maxX=-Infinity,minY=Infinity,maxY=-Infinity;
      for(const [x,y]of building.points){minX=Math.min(minX,x);maxX=Math.max(maxX,x);minY=Math.min(minY,y);maxY=Math.max(maxY,y);}
      return {building,minX,maxX,minY,maxY,order};
    });
    if(this.entries.length<32)return;
    let x0=Infinity,x1=-Infinity,y0=Infinity,y1=-Infinity;
    for(const entry of this.entries){x0=Math.min(x0,entry.minX);x1=Math.max(x1,entry.maxX);y0=Math.min(y0,entry.minY);y1=Math.max(y1,entry.maxY);}
    this.bounds=[x0,y0,x1,y1];this.cell=Math.max(10,Math.sqrt(Math.max(1,(x1-x0)*(y1-y0))/this.entries.length));
    for(const entry of this.entries){
      const minX=Math.floor(entry.minX/this.cell),maxX=Math.floor(entry.maxX/this.cell),minY=Math.floor(entry.minY/this.cell),maxY=Math.floor(entry.maxY/this.cell);
      if((maxX-minX+1)*(maxY-minY+1)>4096){this.large.push(entry);continue;}
      for(let y=minY;y<=maxY;y++)for(let x=minX;x<=maxX;x++){const key=`${x},${y}`;this.bins.set(key,[...this.bins.get(key)??[],entry]);}
    }
  }
  point(x:number,y:number):IndexedBuilding[]{
    if(this.entries.length<32)return this.entries;
    return [...this.large,...this.bins.get(`${Math.floor(x/this.cell)},${Math.floor(y/this.cell)}`)??[]].sort((a,b)=>a.order-b.order);
  }
  ray(sx:number,sy:number,x:number,y:number):IndexedBuilding[]{
    if(this.entries.length<32)return this.entries;
    const dx=x-sx,dy=y-sy,[x0,y0,x1,y1]=this.bounds;let enter=0,leave=1;
    for(const [start,delta,min,max]of [[sx,dx,x0,x1],[sy,dy,y0,y1]]){
      if(delta===0){if(start<min||start>max)return [];continue;}
      const a=(min-start)/delta,b=(max-start)/delta;enter=Math.max(enter,Math.min(a,b));leave=Math.min(leave,Math.max(a,b));if(enter>leave)return [];
    }
    let col=Math.floor((sx+dx*enter)/this.cell),row=Math.floor((sy+dy*enter)/this.cell);
    const endCol=Math.floor((sx+dx*leave)/this.cell),endRow=Math.floor((sy+dy*leave)/this.cell),stepX=Math.sign(dx),stepY=Math.sign(dy);
    const nextX=()=>stepX?((col+(stepX>0?1:0))*this.cell-sx)/dx:Infinity;
    const nextY=()=>stepY?((row+(stepY>0?1:0))*this.cell-sy)/dy:Infinity;
    let tx=nextX(),ty=nextY();const found=new Map(this.large.map(entry=>[entry.order,entry]));
    const maxSteps=Math.abs(endCol-col)+Math.abs(endRow-row)+3;
    for(let i=0;i<maxSteps;i++){
      for(const entry of this.bins.get(`${col},${row}`)??[])found.set(entry.order,entry);
      if(col===endCol&&row===endRow)break;
      if(tx<ty){col+=stepX;tx=nextX();}else if(ty<tx){row+=stepY;ty=nextY();}else{col+=stepX;row+=stepY;tx=nextX();ty=nextY();}
    }
    return [...found.values()].sort((a,b)=>a.order-b.order);
  }
}
