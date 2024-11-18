create database SistemaConsultoria

use SistemaConsultoria

 
 
 -- Tabla Roles
CREATE TABLE Roles (
    RolID INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(200)
);

INSERT INTO Roles (Nombre, Descripcion)
VALUES 
('Administrador', 'Usuario con acceso total al sistema, responsable de la administración y configuración general.'),
('Gerente de Proyecto', 'Usuario encargado de la gestión y supervisión de proyectos, con permisos para asignar tareas y recursos.'),
('Consultor', 'Usuario asignado a tareas específicas dentro de proyectos, con permisos de acceso limitado.');


select * from Roles;

-- Tabla Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    RolID INT FOREIGN KEY REFERENCES Roles(RolID),
    FechaCreacion DATETIME DEFAULT GETDATE()
);


INSERT INTO Usuarios (Nombre, Email, PasswordHash, RolID)
VALUES 
('Juan Gonzales', 'juan@gmail.com', 'juan123', 1),
('Cesar Farfan', 'cesar@gmail.com', 'cesar123', 2);

select * from Usuarios;

-- Tabla Proyectos
CREATE TABLE Proyectos (
    ProyectoID INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    FechaInicio DATE,
    FechaFin DATE,
    Prioridad CHAR(1) CHECK (Prioridad IN ('A', 'M', 'B')), -- 'A' = Alta, 'M' = Media, 'B' = Baja
    Estado CHAR(1) CHECK (Estado IN ('P', 'E', 'C')),       -- 'P' = Pendiente, 'E' = En progreso, 'C' = Completado
    GerenteID INT FOREIGN KEY REFERENCES Usuarios(UsuarioID),
    PorcentajeCompleto DECIMAL(5,2) DEFAULT 0
);

INSERT INTO Proyectos (Nombre, Descripcion, FechaInicio, FechaFin, Prioridad, Estado, GerenteID, PorcentajeCompleto)
VALUES 
('Desarrollo de Aplicación Móvil', 
 'Proyecto para desarrollar una aplicación móvil para la gestión de inventarios.', 
 '2024-01-15', 
 '2024-06-15', 
 'A', 
 'P', 
 2,  -- ID del gerente (asegúrate de que exista este ID en la tabla Usuarios)
 10.00);


-- Tabla Tareas
CREATE TABLE Tareas (
    TareaID INT PRIMARY KEY IDENTITY,
    ProyectoID INT FOREIGN KEY REFERENCES Proyectos(ProyectoID),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    FechaInicio DATE,
    FechaFin DATE,
    Estado CHAR(1) CHECK (Estado IN ('P', 'E', 'C')), -- 'P' = Pendiente, 'E' = En progreso, 'C' = Completada
    UsuarioAsignadoID INT FOREIGN KEY REFERENCES Usuarios(UsuarioID)
);

-- Tabla Recursos
CREATE TABLE Recursos (
    RecursoID INT PRIMARY KEY IDENTITY,
    ProyectoID INT FOREIGN KEY REFERENCES Proyectos(ProyectoID),
    Tipo CHAR(1) CHECK (Tipo IN ('H', 'M')), -- 'H' = Humano, 'M' = Material
    Nombre NVARCHAR(100) NOT NULL,
    Costo DECIMAL(10, 2) DEFAULT 0,
    TiempoAsignado DECIMAL(5, 2) DEFAULT 0,
    UsuarioID INT FOREIGN KEY REFERENCES Usuarios(UsuarioID) -- Opcional si el recurso es humano
);

-- Tabla Notificaciones
CREATE TABLE Notificaciones (
    NotificacionID INT PRIMARY KEY IDENTITY,
    UsuarioID INT FOREIGN KEY REFERENCES Usuarios(UsuarioID),
    Tipo CHAR(1) CHECK (Tipo IN ('T', 'P')), -- 'T' = Tarea, 'P' = Proyecto
    Mensaje NVARCHAR(255) NOT NULL,
    FechaEnvio DATETIME DEFAULT GETDATE(),
    Estado CHAR(1) DEFAULT 'P' CHECK (Estado IN ('P', 'L')) -- 'P' = Pendiente, 'L' = Leído
);
