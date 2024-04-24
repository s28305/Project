USE master;

DECLARE @sql NVARCHAR(MAX) = '';

SELECT @sql += 'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) + ' DROP CONSTRAINT ' + QUOTENAME(name) + ';' + CHAR(13)
FROM sys.foreign_keys;

EXEC sp_executesql @sql;

DECLARE @tableSql NVARCHAR(MAX) = '';

SELECT @tableSql += 'DROP TABLE ' + QUOTENAME(schema_name(schema_id)) + '.' + QUOTENAME(name) + ';' + CHAR(13)
FROM sys.tables;

EXEC sp_executesql @tableSql;

DECLARE @procedureSql NVARCHAR(MAX) = '';

SELECT @procedureSql += 'DROP PROCEDURE ' + QUOTENAME(schema_name(schema_id)) + '.' + QUOTENAME(name) + ';' + CHAR(13)
FROM sys.procedures;

EXEC sp_executesql @procedureSql;