using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiDemo.Models
{
    [Table("contaminants")]
    public class Contaminant
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Column("name")]
        public required string Name { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
    [Table("soil_types")]
    public class SoilType
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Column("name")]
        public required string Name { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
    [Table("pathways")]
    public class Pathway
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Column("name")]
        public required string Name { get; set; }
        [Column("description")]
        public required string Description { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
    [Table("guidelinevalues")]
    public class GuidelineValue
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("contaminant_id")]
        public int ContaminantId { get; set; }
        [Required]
        [Column("soil_type_id")]
        public int SoilTypeId { get; set; }
        [Required]
        [Column("pathway_id")]
        public int PathwayId { get; set; }
        [Required]
        [Column("guideline_value")]
        public decimal Guideline_Value { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
    [Table("measurements")]
    public class Measurement
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("contaminant_id")]
        public int ContaminantId { get; set; }
        [Required]
        [Column("measured_value")]
        public decimal MeasuredValue { get; set; }
        [Required]
        [Column("soil_type_id")]
        public int SoilTypeId { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}