## Java-SQL≈‰÷√
### SQL≈‰÷√1
```
<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE mapper PUBLIC ""-//mybatis.org//DTD Mapper 3.0//EN"" ""http://mybatis.org/dtd/mybatis-3-mapper.dtd"">
<mapper namespace=""com.breezee.part.mapper.AreaMapper"">
    <select id=""queryArea"" resultType=""com.breezee.part.entity.EntArea"">
      SELECT  *  FROM tb_area  ORDER BY priority DESC 
    </select>
    <select id=""queryAreaById"" resultType=""com.breezee.part.entity.EntArea"">
      SELECT  *  FROM tb_area WHERE area_id = #{areaId}
    </select>
    <insert id=""insertArea"" useGeneratedKeys=""true"" keyProperty=""areaId""
            keyColumn=""area_id"" parameterType=""com.breezee.part.entity.EntArea"">
      INSERT INTO  
      tb_area (area_name, priority, create_time, last_edit_time)
      VALUES (#{areaName},#{priority},#{createTime},#{lastEditTime})
    </insert>  
    <update id=""updateArea"" parameterType=""com.breezee.part.entity.EntArea"">
        UPDATE tb_area 
        <set>
            <if test=""areaName != null"">area_name = #{areaName}</if>
            <if test=""priority != null"">, priority = #{priority}</if>
            <if test=""lastEditTime != null"">, last_edit_time = #{lastEditTime}</if>
        </set>
        WHERE area_id = #{areaId} 
    </update>   
    <delete id=""deleteAreaById"">
        DELETE FROM 
        tb_area
        WHERE area_id = #{areaId}
    </delete>   
</mapper>	
```

